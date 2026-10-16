# Fix plan — remaining bugs outside the two earlier plans

> Companion to `frmsothutien-bug-fixes.md` and `other-forms-bug-fixes.md` (both already executed on
> branch `fix/all-forms`). This audit covers what those plans did **not** read: `Service/`, `Utils/`,
> `Facade/`, `Service/SMS/`, `Components/`, `Workflow/`, `UserControls/`, `Program.cs`, the
> `ACG.Core.WinForm.*` libraries, the remaining `DanhMuc` forms, `FrmPreference` and
> `FrmSoThuTien.GenerateSoThuTien.cs` / `.Report.cs`.
>
> **Written to be executed by a small/cheap model, one task per session.** Every task gives the exact
> file, the exact text to find, and the exact replacement. No design decisions are left to the
> executor — the owner's decisions (2026-10-16) are recorded under "Owner decisions" at the end.
>
> **Reference database:** `DB\qlmn_backup_202610121440249028.sql` (MySqlBackup dump of 12/10/2026,
> MySQL 5.7). The older dumps were outdated and removed. All schema/procedure facts below were
> re-checked against this dump. It contains **real student and parent data**: restore it only into a
> local test database, and never paste its rows into chat, logs, commits or anything published.

## Progress checklist

Tick a box (and nothing else in this file) when a task is committed.

- [x] T1  Global exception handler + culture on worker threads — `Program.cs`
- [x] T2  Log full exception text (inner exceptions were lost) — `LoggerFacade`, `TraceListenerLogger`
- [x] T3  "Tạo sổ thu tiền" crash / orphan rows — `SoThuTienService`
- [x] T4  No MessageBox on the worker thread in "Tạo sổ thu tiền" — `FrmSoThuTien.GenerateSoThuTien.cs`
- [x] T5  Truy thu recalculation corrupts later months — `TruyThuService` **(gated: run the SQL check first)**
- [x] T6  SMS: app crash on serial-port error, wrong recipient for numbers with "-" — `USB3GSms`, `FrmTinNhanPhuHuynh`
- [x] T7  User privileges leak from one user to the next in the edit popup — `UCEditFormUser` **(reproduce first)**
- [x] T8  Lookup cache never refreshes (fee rules, categories, names) — `Components/Data.Static/*`, 5 DanhMuc forms
- [x] T9  "Lưu" in Preferences silently drops two settings — `FrmPreference`
- [x] T10 `DateStartOfMonth` returns the wrong day — `DateTimeUtil` + its 2 callers
- [x] T11 "In giấy báo nộp tiền" crashes on NULL cells — `FrmSoThuTien.Report.cs`
- [x] T12 NullReference / KeyNotFound traps in the lookup helpers — `StaticDataMap`, `StaticDataUtil`
- [x] T13 Google Drive backup: clearer failure, path independent of working dir — `GDriveFacade`
- [x] T14 Latent crash traps in popup/activity/mediator code — 4 small files
- [x] T15 Undisposed DbContexts — `PhieuThuService`, `SoThuTienService`
- [x] T16 Bảng kê thu học phí counts only tuition receipts — `SoThuTienService`
- [x] T17 Password hashing: salted PBKDF2 + automatic upgrade of old MD5 hashes at login — `PasswordUtil`, `AuthenService`, `FrmLogin`, `UCEditFormUser` **(medium-hard)**

Order matters only for: **T1 and T2 first** (so every later failure is logged), **T3 before T4**
(same feature, test them together). Everything else is independent.

---

## Rules for the executing model (read before every task)

1. **One task = one session = one commit.** Do not start the next task in the same session.
2. Work on branch `fix/remaining-bugs` (create it from `fix/all-forms` on the first task:
   `git checkout fix/all-forms && git checkout -b fix/remaining-bugs`).
3. **Read the whole target file before editing.** Every "Find" block below must match the file
   **exactly once**. If it does not match (someone changed the file), **stop and report** — do not
   improvise a different edit.
4. Change only what the task says. No refactoring, no renaming, no reformatting, no "while I'm here".
5. Files are **UTF-8 with BOM and CRLF line endings**. Keep both. Do not convert line endings.
6. Language level is pre-C# 6 except string interpolation: **no `?.`, no `??`, no `nameof`, no
   `=>` members.** Lambdas (`x => ...`) are fine.
7. Never edit `*.Designer.cs`, `*.resx`, `Dao/QLMNModel*.cs` or the generated `Dao/<table>.cs` files.
8. UI strings and comments are **Vietnamese** — copy them exactly as written in this plan.
9. **Never open, print or edit `QLMamNon/client_secret.json`.** Don't copy anything from `app.config`
   connection strings into code, logs or commit messages.
10. Build after every task (the `dotnet` CLI cannot build this solution):
    ```
    "C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe" QLMamNon.sln /t:Build /p:Configuration=Debug
    ```
    Warning MSB4078 from `Setup\Setup.vdproj` is expected. **Any new error or warning = task not done.**
11. There are no automated tests. Do the task's manual check against a **restored copy** of the DB
    (`DB\qlmn_backup_202610121440249028.sql`, e.g. `mysql -u root -p qlmamnon_test < DB\qlmn_backup_202610121440249028.sql`
    after `CREATE DATABASE qlmamnon_test CHARACTER SET utf8;`, with the app's connection strings
    temporarily pointed at it) — never the live school database. If you cannot run
    the app, say so plainly in your report: "built, not run".
12. Commit message: the one given in the task, ending with the attribution lines your harness requires.

---

## T1 — Global exception handler + culture on worker threads

**Difficulty:** easy. **File:** `QLMamNon/Program.cs`

**Problem.** (a) There is no `Application.ThreadException` / `AppDomain.UnhandledException` handler,
so any unhandled exception is a raw crash dialog and is never logged. (b) The `vi-VN` culture with `.`
thousands / `,` decimal is set only on the UI thread; `BackgroundWorker` and `Task.Run` threads (sổ
thu tiền generation, backup, SMS) parse/format numbers and dates with the machine's default culture.

**Step 1.** Add a using. Find:
```csharp
using System.Globalization;
```
Replace with:
```csharp
using System.Globalization;
using QLMamNon.Facade;
```

**Step 2.** Find:
```csharp
            Thread.CurrentThread.CurrentCulture = newCulture;
            Thread.CurrentThread.CurrentUICulture = newCulture;
```
Replace with:
```csharp
            Thread.CurrentThread.CurrentCulture = newCulture;
            Thread.CurrentThread.CurrentUICulture = newCulture;

            // Áp dụng cùng culture cho các thread nền (BackgroundWorker, Task.Run) để đọc/ghi số và ngày
            // giống hệt UI thread.
            CultureInfo.DefaultThreadCurrentCulture = newCulture;
            CultureInfo.DefaultThreadCurrentUICulture = newCulture;

            // Lưới an toàn cuối cùng: ghi log và báo lỗi thay vì để ứng dụng hiện hộp thoại crash.
            // Phải đăng ký trước khi tạo bất kỳ form nào.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
```

**Step 3.** Find (end of `Main` and of the class):
```csharp
            Application.Run(new FrmMain());
        }
```
Replace with:
```csharp
            Application.Run(new FrmMain());
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            LoggerFacade.Error(e.Exception, "Program");
            MessageBox.Show(String.Format("Xuất hiện lỗi không mong muốn. Thao tác vừa rồi có thể chưa được thực hiện.\n\n{0}", e.Exception.Message),
                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            // Lỗi trên thread nền: tiến trình vẫn sẽ dừng, nhưng ít nhất lỗi được ghi vào log.
            Exception ex = e.ExceptionObject as Exception;

            if (ex != null)
            {
                LoggerFacade.Error(ex, "Program");
            }
        }
```

**Check.** Build. Run the app, open and close a few forms — nothing changes visibly. (Optional: in a
throwaway local edit, throw from a button click and confirm a Vietnamese error box appears and
`application.log` gets the stack trace; revert that edit.)

**Commit:** `Add global exception handler and apply vi-VN culture to worker threads`

---

## T2 — Log the full exception (inner exceptions were dropped)

**Difficulty:** easy. **Files:** `QLMamNon/Facade/LoggerFacade.cs`,
`ACG.Core.WinForm.Logging/TraceListenerLogger.cs`

**Problem.** `TraceListenerLogger.Error(Exception)` writes only `ex.Message` + `ex.StackTrace`. EF
errors (`DbUpdateException`, `UpdateException`) put the real cause in `InnerException`, so the log
never says *why* a save failed. `LoggerFacade.Error(Exception, string module)` also ignores `module`.

**Step 1.** In `TraceListenerLogger.cs` find:
```csharp
            WriteEntry(string.Format("{0}\n{1}", ex.Message, ex.StackTrace), "error");
```
Replace with:
```csharp
            // ToString() gồm cả InnerException và stack trace của từng lớp lỗi.
            WriteEntry(ex.ToString(), "error");
```

**Step 2.** In `LoggerFacade.cs` find:
```csharp
        public static void Error(Exception ex, string module)
        {
            logger.Error(ex);
        }
```
Replace with:
```csharp
        public static void Error(Exception ex, string module)
        {
            logger.Error(string.Format("{0}-{1}", module, ex));
        }
```

**Check.** Build (both projects compile). Trigger any handled error (e.g. save a duplicate in a danh
mục) and confirm `application.log` now shows `FrmXxx-System.Data.Entity...` followed by the inner
exception text.

**Commit:** `Log full exception text including inner exceptions and module`

---

## T3 — "Tạo sổ thu tiền": crash on renumbering, crash on student without class, orphan rows

**Difficulty:** medium (4 small edits, one file). **File:** `QLMamNon/Service/Data/SoThuTienService.cs`

**Problems.**
- **(a) Crash — High.** `getAndSortPrevMonthViewBangThuTienRows` loads last month's rows **tracked**
  (default `MergeOption.AppendOnly`) and then renumbers them: `sortedViewBangThuTienRows.Values[i].STT = ...`.
  `viewbangthutien` is a read-only view entity set (DefiningQuery, see CLAUDE.md), so the next
  `entities.SaveChanges()` in `GenerateSoThuTienByHocSinhAndLopAndNgayTinh` throws
  *"Unable to update the EntitySet 'viewbangthutien' because it has a DefiningQuery…"*. It happens
  whenever renumbering changes a value: the class already has some rows this month (adding late
  students), or last month's STT has gaps (a student left).
- **(b) Crash — Medium.** In the same method, `hocSinhIdsToHocSinhLops[viewBangThuTienRow.HocSinhId]`
  throws `KeyNotFoundException` for a selected student that had a row last month but has no
  `hocsinh_lop` valid on `ngayTinh`. The two callers' loops already guard with `ContainsKey`; this one
  does not.
- **(c) Data — Medium.** `GenerateSoThuTienByHocSinhAndLopAndNgayTinh` inserts and **saves** the
  `bangthutien` row first, and only then looks up the class's khối; if there is none it throws, leaving
  a `bangthutien` row with no `bangthutien_khoanthu` lines. Check first, insert after.
- **(d) Leak — Low.** `GenerateSoThuTienByHocSinhRows` never disposes its `qlmamnonEntities`.

**Step 1 (d).** Find:
```csharp
        public int GenerateSoThuTienByHocSinhRows(DateTime ngayTinh, List<hocsinh> hocSinhRows)
        {
            qlmamnonEntities entities = StaticDataFacade.GetQLMNEntities();
            List<viewbangthutien> viewBangThuTienTable = entities.getViewBangThuTienByNgayTinhAndLop(ngayTinh, null).ToList();
```
Replace with:
```csharp
        public int GenerateSoThuTienByHocSinhRows(DateTime ngayTinh, List<hocsinh> hocSinhRows)
        {
            using (qlmamnonEntities entities = StaticDataFacade.GetQLMNEntities())
            {
                return this.generateSoThuTienByHocSinhRows(entities, ngayTinh, hocSinhRows);
            }
        }

        private int generateSoThuTienByHocSinhRows(qlmamnonEntities entities, DateTime ngayTinh, List<hocsinh> hocSinhRows)
        {
            List<viewbangthutien> viewBangThuTienTable = entities.getViewBangThuTienByNgayTinhAndLop(ngayTinh, null).ToList();
```
(The rest of the old method body stays exactly as it is — it is now the body of the private method.)

**Step 2 (a).** Find:
```csharp
            List<viewbangthutien> prevMonthViewBangThuTienTable = entities.getViewBangThuTienByNgayTinhAndLop(ngayTinh.AddMonths(-1), null).ToList();
```
Replace with:
```csharp
            // NoTracking: STT của các dòng này được đánh lại trong bộ nhớ ở dưới. viewbangthutien là
            // entity set chỉ đọc, nếu để EF theo dõi thì SaveChanges() khi tạo sổ sẽ throw UpdateException.
            List<viewbangthutien> prevMonthViewBangThuTienTable = entities.getViewBangThuTienByNgayTinhAndLop(ngayTinh.AddMonths(-1), null, MergeOption.NoTracking).ToList();
```
(`using System.Data.Entity.Core.Objects;` is already at the top of the file.)

**Step 3 (b).** Find:
```csharp
                if (!hocSinhIds.Contains(viewBangThuTienRow.HocSinhId))
                {
                    continue;
                }

                int lopId = hocSinhIdsToHocSinhLops[viewBangThuTienRow.HocSinhId].LopId;
```
Replace with:
```csharp
                if (!hocSinhIds.Contains(viewBangThuTienRow.HocSinhId))
                {
                    continue;
                }

                // Học sinh không thuộc lớp nào vào ngayTinh thì không có lớp để xếp STT.
                if (hocSinhIdsToHocSinhLops == null || !hocSinhIdsToHocSinhLops.ContainsKey(viewBangThuTienRow.HocSinhId))
                {
                    continue;
                }

                int lopId = hocSinhIdsToHocSinhLops[viewBangThuTienRow.HocSinhId].LopId;
```

**Step 4a (c).** Find:
```csharp
            String ghiChu = "";
            bangthutien bangThuTienRow = new bangthutien()
```
Replace with:
```csharp
            String ghiChu = "";

            // Kiểm tra khối trước khi ghi: nếu throw sau SaveChanges() sẽ để lại một bangthutien không có khoản thu.
            lop_khoi lopKhoi = entities.getLopKhoiByLopId(lopId).FirstOrDefault();

            if (lopKhoi == null)
            {
                throw new Exception("Không tìm thấy khối cho lớp được chọn");
            }

            bangthutien bangThuTienRow = new bangthutien()
```

**Step 4b (c).** Find:
```csharp
            int bangThuTienId = bangThuTienRow.BangThuTienId;
            lop_khoi lopKhoi = entities.getLopKhoiByLopId(lopId).FirstOrDefault();

            if (lopKhoi != null)
            {
                int khoiId = lopKhoi.KhoiId;
                generateBangThuTienKhoanThu(entities, bangThuTienId, khoiId, ngayTinh, preMonthViewBangThuTien);
            }
            else
            {
                throw new Exception("Không tìm thấy khối cho lớp được chọn");
            }
        }
```
Replace with:
```csharp
            int bangThuTienId = bangThuTienRow.BangThuTienId;
            generateBangThuTienKhoanThu(entities, bangThuTienId, lopKhoi.KhoiId, ngayTinh, preMonthViewBangThuTien);
        }
```

**Check.** Build. Restored DB: open **Sổ thu tiền**, pick a class whose previous month exists and
where one student left last month (gap in STT) **or** generate for part of the class first and then
click **Tạo sổ thu tiền** again for the rest. Before the fix this shows the "DefiningQuery" error;
after, rows are created and STT continues after the existing ones. Run T4's check right after.

**Commit:** `Fix sổ thu tiền generation: untracked prev-month rows, missing-class guard, check khối before insert`

---

## T4 — "Tạo sổ thu tiền": no MessageBox on the worker thread

**Difficulty:** easy. **File:** `QLMamNon/Forms/ThuChi/FrmSoThuTien.GenerateSoThuTien.cs`

**Problem.** `generateSoThuTiens` is a `BackgroundWorker.DoWork` handler and calls
`XtraMessageBox.Show` from the worker thread (cross-thread UI, can hang or show behind the main
window), and the error is never logged. Let the exception flow to `RunWorkerCompleted` (`e.Error`),
which runs on the UI thread.

**Step 1.** Find:
```csharp
using QLMamNon.Dao;
using QLMamNon.Facade;
```
Replace with:
```csharp
using QLMamNon.Constant;
using QLMamNon.Dao;
using QLMamNon.Facade;
```
(Skip this step if `using QLMamNon.Constant;` is already present.)

**Step 2.** Find the whole block from `private void generateSoThuTiens(` to the end of
`generatedSoThuTiens` — currently:
```csharp
        private void generateSoThuTiens(object sender, DoWorkEventArgs e)
        {
            try
            {
                this.persistNgayTinhAndLop();
                List<hocsinh> hocSinhRows = e.Argument as List<hocsinh>;
                SoThuTienService soThuTienService = new SoThuTienService();
                int generatedRowsCount = soThuTienService.GenerateSoThuTienByHocSinhRows(ngayTinh, hocSinhRows);

                bangthutiengenhistory bangThuTienGenHistory = new bangthutiengenhistory()
                {
                    NgayTinh = ngayTinh,
                    LopId = lop,
                    DateCreated = DateTime.Now,
                    SoTienAnSang = Settings.Default.TienAnSang,
                    SoTienAnToi = Settings.Default.TienAnToi,
                    SoTienAnChinh = Settings.Default.TienAnChinh
                };

                using (qlmamnonEntities entities = StaticDataFacade.GetQLMNEntities())
                {
                    entities.bangthutiengenhistories.Add(bangThuTienGenHistory);
                    entities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void generatedSoThuTiens(object sender, RunWorkerCompletedEventArgs e)
        {
            this.HideGridLoadingPanel();
            this.loadViewBangThuTiens(this.ngayTinh, this.lop);
        }
```
Replace with:
```csharp
        private void generateSoThuTiens(object sender, DoWorkEventArgs e)
        {
            // Chạy trên worker thread: không được hiện MessageBox ở đây. Lỗi ném ra sẽ được
            // BackgroundWorker chuyển sang generatedSoThuTiens qua e.Error (chạy trên UI thread).
            this.persistNgayTinhAndLop();
            List<hocsinh> hocSinhRows = e.Argument as List<hocsinh>;
            SoThuTienService soThuTienService = new SoThuTienService();
            int generatedRowsCount = soThuTienService.GenerateSoThuTienByHocSinhRows(ngayTinh, hocSinhRows);

            bangthutiengenhistory bangThuTienGenHistory = new bangthutiengenhistory()
            {
                NgayTinh = ngayTinh,
                LopId = lop,
                DateCreated = DateTime.Now,
                SoTienAnSang = Settings.Default.TienAnSang,
                SoTienAnToi = Settings.Default.TienAnToi,
                SoTienAnChinh = Settings.Default.TienAnChinh
            };

            using (qlmamnonEntities entities = StaticDataFacade.GetQLMNEntities())
            {
                entities.bangthutiengenhistories.Add(bangThuTienGenHistory);
                entities.SaveChanges();
            }
        }

        private void generatedSoThuTiens(object sender, RunWorkerCompletedEventArgs e)
        {
            this.HideGridLoadingPanel();

            if (e.Error != null)
            {
                LoggerFacade.Error(e.Error, AppForms.FormSoThuTien);
                XtraMessageBox.Show(String.Format("Tạo sổ thu tiền không thành công.\n\n{0}", e.Error.Message),
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.loadViewBangThuTiens(this.ngayTinh, this.lop);
        }
```

**Check.** Build. Restored DB: temporarily delete the `lop_khoi` row of one class (on the restored
copy only), generate for that class → a Vietnamese error box appears on top of the main window, the
log has the stack trace, and (thanks to T3) no new `bangthutien` row exists for those students.
Restore the row.

**Commit:** `Report sổ thu tiền generation errors on the UI thread and log them`

---

## T5 — Truy thu recalculation always fails once next month's sheet exists

**Difficulty:** hard — **use a stronger model or have the owner review the diff.**
**Files:** `QLMamNon/Service/Data/TruyThuService.cs`, `QLMamNon/Forms/ThuChi/FrmTaoPhieuThu.cs`

**Gate — do this first:** `ThanhTien` must be a `[NotMapped]` property in
`QLMamNon/Dao/viewbangthutien.Partial.cs` and must **not** appear in `QLMamNon/Dao/viewbangthutien.cs`.
(In the reference dump the view has no `ThanhTien` column.) If that is not the case, **stop** and report.

**Problem — High (money data / crash).** After a phiếu thu is saved (`FrmTaoPhieuThu`) or deleted
(`FrmPhieuThu`) for month M, `UpdateSoTienTruyThuForBangThuTienNextMonths` is meant to correct the
arrears (`SoTienTruyThu`) of month M+1 and later. Whenever a sheet for M+1 already exists (any receipt
entered or deleted for a past month), it fails:
1. It reads `entities.Entry(row).OriginalValues["ThanhTien"]`. `ThanhTien` is `[NotMapped]`, so EF
   throws `ArgumentException` ("property … is not mapped"). The truy thu of later months is **never**
   updated. In `FrmTaoPhieuThu` the call has no try/catch: the receipt is already saved, then the app
   shows a crash dialog — the user is likely to save the same receipt again (duplicate). In
   `FrmPhieuThu` every such delete ends with an error box.
2. Even without the exception the logic is wrong: M+1's arrears must come from month **M** (the month
   of the receipt), but the code evaluates `ngay.AddMonths(-1)` (month M−1), so the receipt just
   entered is not part of the calculation. (Both defects were copied verbatim from the original
   `FrmTaoPhieuThu` code, commit `d8a4e55`.)

**Fix.** Compute the new arrears of M+1 with the same formula the grids use
(`BangThuTienUtil.CalculateTruyThu(rowM+1, evaluatedRowM)`), compare it with the **stored**
`SoTienTruyThu` of M+1, and add the difference to every sheet from M+1 onward (arrears cascade 1:1).

**Step 1.** Find the whole public method, from
```csharp
        public void UpdateSoTienTruyThuForBangThuTienNextMonths(qlmamnonEntities entities, DateTime ngay, int hocSinhId)
        {
```
down to and including its closing brace (the line `        }` right before
`        private Dictionary<int, viewbangthutien> evaluatePrevMonthViewBangThuTienTable(`). Replace the whole
method with:
```csharp
        public void UpdateSoTienTruyThuForBangThuTienNextMonths(qlmamnonEntities entities, DateTime ngay, int hocSinhId)
        {
            // Dòng bảng thu tiền của tháng sau (M+1). NoTracking: chỉ đọc, không bao giờ lưu qua view.
            List<viewbangthutien> table = entities
                .getViewBangThuTienByNgayTinhAndHocSinhId(ngay.AddMonths(1), hocSinhId, MergeOption.NoTracking).ToList();

            if (ListUtil.IsEmpty(table))
            {
                return;
            }

            viewbangthutien nextMonthRow = table[0];

            // Truy thu của tháng M+1 là số còn nợ của tháng M (tháng của ngay), nên "tháng trước" của
            // dòng này là ngay, không phải ngay.AddMonths(-1).
            Dictionary<int, viewbangthutien> thisMonthRowDictionary = this.evaluatePrevMonthViewBangThuTienTable(entities, ngay, hocSinhId);
            viewbangthutien thisMonthRow = thisMonthRowDictionary != null && thisMonthRowDictionary.ContainsKey(hocSinhId)
                ? thisMonthRowDictionary[hocSinhId] : null;

            // So với SoTienTruyThu đang lưu trong bangthutien. Không dùng ThanhTien: đó là thuộc tính
            // [NotMapped], EF không có giá trị gốc cho nó (OriginalValues["ThanhTien"] sẽ throw).
            long soTienTruyThuDangLuu = nextMonthRow.SoTienTruyThu == null ? 0 : nextMonthRow.SoTienTruyThu.Value;
            long soTienTruyThuMoi = BangThuTienUtil.CalculateTruyThu(nextMonthRow, thisMonthRow);
            long chenhLech = soTienTruyThuMoi - soTienTruyThuDangLuu;

            if (chenhLech == 0)
            {
                return;
            }

            // Thủ tục cộng chênh lệch cho mọi bangthutien của học sinh có NgayTinh > ngày truyền vào.
            // Truyền ngày cuối tháng M (không có giờ) để cập nhật từ tháng M+1 trở đi mà không đụng tháng M.
            DateTime ngayCuoiThang = new DateTime(ngay.Year, ngay.Month, DateTime.DaysInMonth(ngay.Year, ngay.Month));
            entities.updateSoTienTruyThuByHocSinhAndNgayUpdated(hocSinhId, ngayCuoiThang, chenhLech);
        }
```
Leave `evaluatePrevMonthViewBangThuTienTable` and the `using` lines unchanged (an unused `using` is
fine).

**Step 2.** In `FrmTaoPhieuThu.cs` the receipt is already saved when the truy thu update runs, so a
failure there must be reported, not crash. Find:
```csharp
                using (qlmamnonEntities truyThuEntities = StaticDataFacade.GetQLMNEntities())
                {
                    TruyThuService truyThuService = new TruyThuService();
                    truyThuService.UpdateSoTienTruyThuForBangThuTienNextMonths(truyThuEntities, this.dateNgay.DateTime, hocSinhId.Value);
                }
```
Replace with:
```csharp
                try
                {
                    using (qlmamnonEntities truyThuEntities = StaticDataFacade.GetQLMNEntities())
                    {
                        TruyThuService truyThuService = new TruyThuService();
                        truyThuService.UpdateSoTienTruyThuForBangThuTienNextMonths(truyThuEntities, this.dateNgay.DateTime, hocSinhId.Value);
                    }
                }
                catch (Exception ex)
                {
                    // Phiếu thu đã được lưu ở trên: chỉ báo lỗi, không để người dùng lưu lại lần nữa.
                    LoggerFacade.Error(ex, this.FormKey);
                    MessageBox.Show(String.Format("Đã lưu phiếu thu nhưng không thể cập nhật lại số tiền truy thu của tháng sau.\n\n{0}", ex.Message),
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
```
(`FrmPhieuThu.cs` already wraps its call like this — do not change it.)

**Check (restored DB).** Pick student X with sheets for months M, M+1 **and** M+2.
1. Before: `SELECT NgayTinh, SoTienTruyThu FROM bangthutien WHERE HocSinhId = X AND IsDeleted = 0 ORDER BY NgayTinh;`
2. Create a phiếu thu học phí of 500.000 for X dated in month M.
3. Run the query again. Expected: M unchanged; M+1 and M+2 changed by the **same** amount; M+1's new
   value equals *Thành tiền of X in Sổ thu tiền month M* minus *total paid in month M*. No error box.
4. Delete that phiếu thu in **Danh sách phiếu thu** → M+1 and M+2 each go **up by exactly 500.000**
   compared with step 3.

**Commit:** `Fix truy thu recalculation: no unmapped ThanhTien, use the receipt's month, report failures`

---

## T6 — SMS: crash on serial-port error, wrong recipient for numbers containing "-"

**Difficulty:** easy-medium. **Files:** `QLMamNon/Service/SMS/USB3GSms.cs`,
`QLMamNon/Forms/TinNhan/FrmTinNhanPhuHuynh.cs`

**Problems.**
- **Crash — High.** `SerialPort_ErrorReceived` **throws**. `SerialPort` raises that event on its own
  background thread, so the exception is unhandled and terminates the whole application (a modem
  glitch while sending = app gone).
- **Leak.** Every send creates a new `USB3GSms` that subscribes again to the form's `serialPort1`;
  handlers pile up and are never removed.
- **Wrong recipient — High.** The dictionary key is `"{soDienThoai}-{rowHandle}"` and the number is
  cut at `IndexOf("-")` — the **first** dash. A number typed as `0905-123-456` is sent to `0905`.
  Same bug in the form's log loop.
- **Low.** `isStatusOk` uses `IndexOf("OK") > 0`, so a reply that starts with `OK` counts as failure.

**Step 1.** In `USB3GSms.cs` find:
```csharp
        private void SerialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            throw new Exception($"Error occurs on SerialPort {serialPort.PortName}");
        }
```
Replace with:
```csharp
        private void SerialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            // Sự kiện này chạy trên thread riêng của SerialPort: throw ở đây sẽ làm sập cả ứng dụng.
            // Chỉ ghi log; việc gửi thất bại vẫn được phát hiện qua isStatusOk trong sendSmsMessage.
            LoggerFacade.Error(string.Format("Lỗi trên cổng {0}: {1}", serialPort.PortName, e.EventType), AppForms.FormTinNhanPhuHuynh);
        }
```

**Step 2.** In `USB3GSms.cs` replace the whole `SendSMS` method — find:
```csharp
        public Dictionary<string, bool> SendSMS(Dictionary<string, string> phoneNumberToSmsContentDic)
        {
            Dictionary<string, bool> sendingStatusDic = new Dictionary<string, bool>();

            foreach (var phoneNumberToSmsContent in phoneNumberToSmsContentDic)
            {
                string phoneNumberKey = phoneNumberToSmsContent.Key;

                if (!string.IsNullOrWhiteSpace(phoneNumberKey))
                {
                    string phoneNumber = phoneNumberKey.Substring(0, phoneNumberKey.IndexOf("-"));
                    bool sendingSuccess = sendSmsMessage(phoneNumber, phoneNumberToSmsContent.Value);
                    sendingStatusDic.Add(phoneNumberToSmsContent.Key, sendingSuccess);

                    Thread.Sleep(timeInsecondsToSleepBetweenSms * 1000);
                }
            }

            return sendingStatusDic;
        }
```
Replace with:
```csharp
        public Dictionary<string, bool> SendSMS(Dictionary<string, string> phoneNumberToSmsContentDic)
        {
            Dictionary<string, bool> sendingStatusDic = new Dictionary<string, bool>();

            try
            {
                foreach (var phoneNumberToSmsContent in phoneNumberToSmsContentDic)
                {
                    string phoneNumberKey = phoneNumberToSmsContent.Key;

                    if (!string.IsNullOrWhiteSpace(phoneNumberKey))
                    {
                        string phoneNumber = GetPhoneNumberFromKey(phoneNumberKey);
                        bool sendingSuccess = sendSmsMessage(phoneNumber, phoneNumberToSmsContent.Value);
                        sendingStatusDic.Add(phoneNumberToSmsContent.Key, sendingSuccess);

                        Thread.Sleep(timeInsecondsToSleepBetweenSms * 1000);
                    }
                }
            }
            finally
            {
                // Mỗi lần gửi tạo một USB3GSms mới trên cùng serialPort1 của form: phải gỡ handler,
                // nếu không các handler sẽ chồng lên nhau sau mỗi lần gửi.
                serialPort.ErrorReceived -= SerialPort_ErrorReceived;
            }

            return sendingStatusDic;
        }

        /// <summary>
        /// Khóa có dạng "{soDienThoai}-{rowHandle}". Số điện thoại có thể chứa dấu "-" (ví dụ
        /// "0905-123-456") nên phải cắt ở dấu "-" cuối cùng, sau đó bỏ mọi ký tự không phải chữ số.
        /// </summary>
        public static string GetPhoneNumberFromKey(string phoneNumberKey)
        {
            int separatorIndex = phoneNumberKey.LastIndexOf("-");
            string phoneNumber = separatorIndex < 0 ? phoneNumberKey : phoneNumberKey.Substring(0, separatorIndex);
            return new string(phoneNumber.Where(c => char.IsDigit(c) || c == '+').ToArray());
        }
```

**Step 3.** In `USB3GSms.cs` find:
```csharp
            if (statusMsg.IndexOf("OK", StringComparison.CurrentCultureIgnoreCase) > 0)
```
Replace with:
```csharp
            if (statusMsg.IndexOf("OK", StringComparison.OrdinalIgnoreCase) >= 0)
```

**Step 4.** In `FrmTinNhanPhuHuynh.cs` find:
```csharp
                string phoneNumber = phoneNumberKey.Substring(0, phoneNumberKey.IndexOf("-"));
```
Replace with:
```csharp
                string phoneNumber = USB3GSms.GetPhoneNumberFromKey(phoneNumberKey);
```

**Check.** Build. Without a modem: in a restored DB give one student the phone `0905-123-456`,
select only that student, send with any COM port. Open `sms_log.txt` next to the exe: the logged
number must be `0905123456` (before the fix: `0905`). The app must not crash if the port errors.

**Commit:** `SMS: log serial-port errors instead of crashing, unsubscribe handler, parse phone numbers containing dashes`

---

## T7 — User privileges leak from one user to the next in the edit popup

**Difficulty:** easy (one line) — **but reproduce first.** **File:** `QLMamNon/UserControls/UCEditFormUser.cs`

**Problem — High (security), needs confirmation.** `FrmUser` assigns **one** `UCEditFormUser`
instance as `CustomEditFormLayout`, so the privilege grid inside it keeps its row selection between
popups. `UCEditFormUser_Enter` only **adds** the current user's privileges to the selection and never
clears the previous user's. Editing user A (many privileges) then user B shows A's privileges ticked
for B; touching any tick then writes the union to B. A brand-new user (no `UserId`) returns early and
inherits the previous selection entirely.

**Reproduce (restored DB) before editing:** open the Người dùng screen (`FrmUser`). Edit a user with many privileges,
close with **Hủy**. Edit a user with few privileges. If the second popup shows the first user's
privileges ticked, the bug is real — continue. If not, **stop and report "not reproducible"**.

**Step 1.** Find:
```csharp
        private void UCEditFormUser_Enter(object sender, EventArgs e)
        {
            this._selectGvMainRowQuietMode = true;
```
Replace with:
```csharp
        private void UCEditFormUser_Enter(object sender, EventArgs e)
        {
            this._selectGvMainRowQuietMode = true;

            // UserControl này được dùng lại cho mọi lần mở popup: xóa lựa chọn của người dùng trước,
            // nếu không quyền của người đó sẽ bị cộng dồn sang người đang sửa.
            this.gvMain.ClearSelection();
```

**Check.** Repeat the reproduction: each popup now shows only that user's privileges; a new user
starts with none ticked. Save a change for user B and confirm in DB
(`SELECT * FROM user_privilege WHERE UserId = B`) that only B's chosen privileges exist.

**Commit:** `Clear privilege selection when the user edit popup opens`

---

## T8 — Lookup cache never refreshes (fee rules, categories, names)

**Difficulty:** medium (many small identical edits). **Files:** 10 files in
`QLMamNon/Components/Data.Static/`, plus `FrmBangTinhPhi.cs`, `FrmKhoanThu.cs`, `FrmPhanLoaiThu.cs`,
`FrmPhanLoaiChi.cs`, `FrmTinhThanhPho.cs` in `QLMamNon/Forms/DanhMuc/`

**Problem — Medium-High.** Lookup lists are cached in `StaticDataFacade` and loaded through
`FrmMain`'s single long-lived `qlmamnonEntities`.
1. Only `FrmTruongHoc`, `FrmKhoiHoc`, `FrmLopHoc` call `StaticDataFacade.Reload(...)` after saving.
   Editing **Bảng tính phí** does not, so `BangThuTienUtil.CalculateSoTienPhi` keeps pro-rating fees
   with the **old rules until the app is restarted**. Same for Khoản thu, Phân loại thu/chi, Tỉnh/TP.
2. Even when `Reload` runs, `Retrieve()` re-queries the **same tracked context**; EF's default
   `AppendOnly` returns the already-loaded instances with their **old values**. So an edited name or
   amount is never seen; only newly added rows appear.

Nothing writes through these cached instances (`grep -rn "\.Attach(" QLMamNon --include=*.cs` →
none; the entity classes have no navigation properties), so loading them untracked is safe.

**Step 1 — untracked loads.** In each file below make exactly this one-line change (add
`.AsNoTracking()` before `.ToList()`; no new `using` is needed):

| File | Find | Replace with |
|---|---|---|
| `BangTinhPhi.cs` | `return entities.bangtinhphis.ToList();` | `return entities.bangtinhphis.AsNoTracking().ToList();` |
| `KhoanThuData.cs` | `return this.entities.khoanthus.ToList();` | `return this.entities.khoanthus.AsNoTracking().ToList();` |
| `KhoiData.cs` | `return this.entities.khois.ToList();` | `return this.entities.khois.AsNoTracking().ToList();` |
| `LopData.cs` | `return this.entities.lops.ToList();` | `return this.entities.lops.AsNoTracking().ToList();` |
| `PhanLoaiChi.cs` | `return this.entities.phanloaichis.ToList();` | `return this.entities.phanloaichis.AsNoTracking().ToList();` |
| `PhanLoaiThu.cs` | `return this.entities.phanloaithus.ToList();` | `return this.entities.phanloaithus.AsNoTracking().ToList();` |
| `PhuongXaData.cs` | `return this.entities.phuongxas.ToList();` | `return this.entities.phuongxas.AsNoTracking().ToList();` |
| `QuanHuyenData.cs` | `return this.entities.quanhuyens.ToList();` | `return this.entities.quanhuyens.AsNoTracking().ToList();` |
| `TinhThanhPhoData.cs` | `return entities.thanhphoes.ToList();` | `return entities.thanhphoes.AsNoTracking().ToList();` |
| `TruongData.cs` | `return this.entities.truongs.ToList();` | `return this.entities.truongs.AsNoTracking().ToList();` |

Do **not** touch `NamHocData.cs` or `TrangThaiHSData.cs` (not DB-backed).

**Step 2 — reload after save, 5 forms.** For each row of this table, open the form file and:

| Form file | `<BINDING>` in the InitForm line | `<KEY>` |
|---|---|---|
| `FrmBangTinhPhi.cs` | `bangTinhPhiRowBindingSource` | `BangTinhPhi` |
| `FrmKhoanThu.cs` | `khoanThuRowBindingSource` | `KhoanThu` |
| `FrmPhanLoaiThu.cs` | `phanLoaiThuRowBindingSource` | `PhanLoaiThu` |
| `FrmPhanLoaiChi.cs` | `phanLoaiChiRowBindingSource` | `PhanLoaiChi` |
| `FrmTinhThanhPho.cs` | `thanhPhoRowBindingSource` | `TinhThanhPho` |

(a) Make sure these four `using` lines exist at the top; add only the missing ones:
```csharp
using System;
using QLMamNon.Components.Data.Static;
using QLMamNon.Constant;
using QLMamNon.Facade;
```
(b) Find (with `<BINDING>` substituted):
```csharp
            this.InitForm(this.btnThem, this.btnChinhSua, this.btnXoa, this.btnLuu, this.btnHuyBo, this.gvMain, this.<BINDING>.DataSource);
        }
```
Replace with (with `<BINDING>` and `<KEY>` substituted):
```csharp
            this.InitForm(this.btnThem, this.btnChinhSua, this.btnXoa, this.btnLuu, this.btnHuyBo, this.gvMain, this.<BINDING>.DataSource);
        }

        // base.onSaving() tự bắt lỗi và báo qua onSavingError, không trả về trạng thái thành công,
        // nên dùng cờ này để biết lần lưu có lỗi hay không.
        private bool savingFailed;

        protected override void onSaving()
        {
            this.savingFailed = false;

            base.onSaving();

            if (this.savingFailed)
            {
                return;
            }

            // Các form khác đọc danh mục này từ cache tĩnh: bỏ cache để lần đọc sau lấy dữ liệu mới.
            StaticDataFacade.Reload(StaticDataKeys.<KEY>);
        }

        protected override void onSavingError(Exception ex)
        {
            this.savingFailed = true;
            FormMainFacade.SetStatusCaption(this.FormKey, StatusCaptions.SaveFailedCaption);
            base.onSavingError(ex);
        }
```
This is the same pattern `FrmTruongHoc.cs` already uses — compare with it if unsure.

**Check.** Build. Restored DB, one app session, no restart:
1. Danh mục → Phân loại thu: rename one category, Lưu. Open Danh sách phiếu thu → the new name shows.
2. Danh mục → Lớp học: rename a class, Lưu. Open Sổ thu tiền → the class combo shows the new name
   (before the fix only a restart showed it).
3. Bảng tính phí: change an amount, Lưu; generate a sổ thu tiền for a student in that khối → the new
   amount is used.

**Commit:** `Refresh lookup caches after danh mục saves and load them untracked`

---

## T9 — "Lưu" in Preferences silently drops two settings

**Difficulty:** easy. **File:** `QLMamNon/Forms/SystemSetting/FrmPreference.cs`

**Problem — Medium.** `AppLannchDate` and `qlmamnonConnectionString1` are **application-scoped**
settings (`app.config` `<applicationSettings>` / `<connectionStrings>`). `Settings.Default.Save()` only
persists user-scoped settings, so the form says "Số liệu đã được lưu thành công" but both values revert
on restart. Worse, the in-memory `AppLannchDate` change alters `CalculateTruyThu` and the cash-book
opening balance for the rest of the session. Make both fields read-only and stop writing them.

**Step 1.** Find:
```csharp
            this.FormKey = AppForms.FormPreference;
            InitializeComponent();
```
Replace with:
```csharp
            this.FormKey = AppForms.FormPreference;
            InitializeComponent();

            // Hai giá trị này là applicationSettings: Settings.Default.Save() không lưu được chúng,
            // nên chỉ hiển thị, không cho sửa (sửa trong app.config rồi khởi động lại ứng dụng).
            this.txtConnectionString.Properties.ReadOnly = true;
            this.dateAppLaunchDate.Properties.ReadOnly = true;
```

**Step 2.** Find:
```csharp
            Settings.Default["qlmamnonConnectionString1"] = txtConnectionString.Text;
            Settings.Default["AppLannchDate"] = dateAppLaunchDate.DateTime;
            Settings.Default["TienAnSang"] = (long)txtSoTienAnSang.Value;
```
Replace with:
```csharp
            Settings.Default["TienAnSang"] = (long)txtSoTienAnSang.Value;
```

**Check.** Build. Open the preferences screen (`FrmPreference`): the two fields show their values but cannot be edited;
change Tiền ăn sáng, Lưu, restart the app → the new Tiền ăn sáng is kept.

**Commit:** `Make application-scoped settings read-only in FrmPreference`

---

## T10 — `DateStartOfMonth` returns the wrong day

**Difficulty:** easy, but do **both** steps. **Files:** `ACG.Core.WinForm.Util/DateTimeUtil.cs`,
`QLMamNon/Forms/ThuChi/FrmSoThuTien.GenerateSoThuTien.cs`

**Problem — Medium.** `DateStartOfMonth` returns `new DateTime(date.Year, date.Month, date.Day, …)` —
the **same day**, not the 1st.
- `FrmBaoCaoSiSoHocSinhTheoLop` uses it for "học sinh đầu tháng": with a date picked mid-month, the
  head-count, new and left counts are wrong.
- `isNeedToGenerateSoThuTiens` uses it to build last month's `NgayTinh`, but `NgayTinh` is always the
  **last day** of the month (`GetNgayTinh`) and `countBangThuTienGenHistoryByLopAndNgayTinh` compares
  the date exactly. It works only by accident when `ngayTinh.AddMonths(-1)` lands on a month end; for
  e.g. 30/04 → 30/03 (March ends on the 31st) it wrongly tells the user to create last month's sheet
  first. Once Step 1 lands it would be wrong **every** month, so Step 2 is mandatory.

**Step 1.** In `DateTimeUtil.cs` find:
```csharp
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
```
Replace with:
```csharp
            return new DateTime(date.Year, date.Month, 1, 0, 0, 0, 0);
```

**Step 2.** In `FrmSoThuTien.GenerateSoThuTien.cs` find:
```csharp
                    DateTime preMonthDate = DateTimeUtil.DateStartOfMonth(ngayTinh.AddMonths(-1));
```
Replace with:
```csharp
                    // NgayTinh luôn là ngày cuối tháng (GetNgayTinh) và thủ tục so sánh ngày chính xác.
                    DateTime preMonthDate = DateTimeUtil.DateEndOfMonth(ngayTinh.AddMonths(-1)).Date;
```

**Check.** Build. `grep -rn "DateStartOfMonth" --include=*.cs .` must show only the definition and the
two callers (`FrmBaoCaoSiSoHocSinhTheoLop.cs`, `FrmSoThuTien.GenerateSoThuTien.cs` validation). Then:
Sổ thu tiền for **04/yyyy** where 03/yyyy was generated → no "tạo sổ tháng trước" message. Báo cáo sĩ
số for a month → HS đầu tháng equals the class size on the 1st.

**Commit:** `Fix DateStartOfMonth to return the 1st and compare previous NgayTinh at month end`

---

## T11 — "In giấy báo nộp tiền" crashes on NULL cells

**Difficulty:** easy. **File:** `QLMamNon/Forms/ThuChi/FrmSoThuTien.Report.cs`

**Problem — Medium.** `fillRptGiayBaoNopTien` calls `.Value` on six nullable columns
(`SoTienTruyThu`, `SoTienAnSangThangTruoc`, `SoTienAnToiThangTruoc`, `SoTienSXThangTruoc`,
`AnSangThangTruoc`, `SXThangTruoc`). One NULL (old row, cleared cell, class without gen-history) →
`InvalidOperationException` and nothing prints. Also `getViewBangThuTienRowsFromMainGrid` adds `null`
for any non-data row handle.

**Step 1.** Find:
```csharp
                    SoTienNoThangTruoc = viewBangThuTienRow.SoTienTruyThu.Value,
                    SoTienAnSangThuaThangTruoc = viewBangThuTienRow.SoTienAnSangThangTruoc.Value,
                    SoTienAnToiThuaThangTruoc = viewBangThuTienRow.SoTienAnToiThangTruoc.Value,
                    SoTienAnTruaThuaThangTruoc = viewBangThuTienRow.SoTienSXThangTruoc.Value,
                    SoXuatAnSangThuaThangTruoc = viewBangThuTienRow.AnSangThangTruoc.Value,
                    SoXuatAnToiThuaThangTruoc = viewBangThuTienRow.AnToiThangTruoc,
                    SoXuatAnTruaThuaThangTruoc = viewBangThuTienRow.SXThangTruoc.Value
```
Replace with:
```csharp
                    SoTienNoThangTruoc = viewBangThuTienRow.SoTienTruyThu == null ? 0 : viewBangThuTienRow.SoTienTruyThu.Value,
                    SoTienAnSangThuaThangTruoc = viewBangThuTienRow.SoTienAnSangThangTruoc == null ? 0 : viewBangThuTienRow.SoTienAnSangThangTruoc.Value,
                    SoTienAnToiThuaThangTruoc = viewBangThuTienRow.SoTienAnToiThangTruoc == null ? 0 : viewBangThuTienRow.SoTienAnToiThangTruoc.Value,
                    SoTienAnTruaThuaThangTruoc = viewBangThuTienRow.SoTienSXThangTruoc == null ? 0 : viewBangThuTienRow.SoTienSXThangTruoc.Value,
                    SoXuatAnSangThuaThangTruoc = viewBangThuTienRow.AnSangThangTruoc == null ? 0 : viewBangThuTienRow.AnSangThangTruoc.Value,
                    SoXuatAnToiThuaThangTruoc = viewBangThuTienRow.AnToiThangTruoc,
                    SoXuatAnTruaThuaThangTruoc = viewBangThuTienRow.SXThangTruoc == null ? 0 : viewBangThuTienRow.SXThangTruoc.Value
```

**Step 2.** Find:
```csharp
                viewbangthutien viewBangThuTienRow = (viewbangthutien)this.GridViewMain.GetRow(this.GridViewMain.GetVisibleRowHandle(i));
                rows.Add(viewBangThuTienRow);
```
Replace with:
```csharp
                viewbangthutien viewBangThuTienRow = this.GridViewMain.GetRow(this.GridViewMain.GetVisibleRowHandle(i)) as viewbangthutien;

                // Dòng nhóm/dòng mới không phải dòng dữ liệu.
                if (viewBangThuTienRow != null)
                {
                    rows.Add(viewBangThuTienRow);
                }
```

**Check.** Build. Restored DB: `UPDATE bangthutien SET SoTienTruyThu = NULL WHERE BangThuTienId = <one row of a test class>;`
then Sổ thu tiền for that class → **In Giấy Báo Nộp Tiền** → previews, that student shows 0 nợ tháng trước.

**Commit:** `Guard nullable columns when printing giấy báo nộp tiền`

---

## T12 — NullReference / KeyNotFound traps in the lookup helpers

**Difficulty:** easy. **Files:** `ACG.Core.WinForm.Data.Static/StaticDataMap.cs`,
`QLMamNon/Utils/StaticDataUtil.cs`

**Problems — Low-Medium.**
- `StaticDataMap.Get`: when a key exists with a `null` value and has a loader, it calls
  `keyDataMap.Add(key, …)` on an existing key → `ArgumentException`. An unknown key throws
  `KeyNotFoundException`.
- `StaticDataUtil.GetTruongNameByTruongId`, `GetTruongByKhoiId`, `GetKhoiNameByKhoiId`,
  `GetLopNameByLopId` dereference `Find(...)` without a null check; they run inside grid
  `CustomColumnDisplayText`, so one id pointing at a deleted khối/lớp/trường crashes the grid paint.
- `GetHocSinhLopsByHocSinhIds` returns `null` for an empty id list while every caller indexes it.

**Step 1.** In `StaticDataMap.cs` find:
```csharp
                    object data = staticData.Retrieve();
                    this.keyDataMap.Add(key, data);
                }
            }

            return this.keyDataMap[key];
```
Replace with:
```csharp
                    object data = staticData.Retrieve();
                    this.keyDataMap[key] = data;
                }
            }

            object value;
            return this.keyDataMap.TryGetValue(key, out value) ? value : null;
```

**Step 2.** In `StaticDataUtil.cs` make these four replacements:

Find:
```csharp
                truong truongRow = truongTable.Find(t => t.TruongId == truongId);
                return truongRow.Name;
```
Replace with:
```csharp
                truong truongRow = truongTable.Find(t => t.TruongId == truongId);
                return truongRow == null ? CommonConstant.EMPTY : truongRow.Name;
```

Find:
```csharp
                truong truongRow = truongTable.Find(t => t.TruongId == truongId.Value);
                return truongRow.Name;
```
Replace with:
```csharp
                truong truongRow = truongTable.Find(t => t.TruongId == truongId.Value);
                return truongRow == null ? CommonConstant.EMPTY : truongRow.Name;
```

Find:
```csharp
                return khoiRow.Name;
```
Replace with:
```csharp
                return khoiRow == null ? CommonConstant.EMPTY : khoiRow.Name;
```

Find:
```csharp
                return lopRow.Name;
```
Replace with:
```csharp
                return lopRow == null ? CommonConstant.EMPTY : lopRow.Name;
```

**Step 3.** In `StaticDataUtil.cs` find:
```csharp
            if (ListUtil.IsEmpty(hocSinhIds))
            {
                return null;
            }

            Dictionary<int, hocsinh_lop> hocSinhIdsToHocSinhLops = new Dictionary<int, hocsinh_lop>();
```
Replace with:
```csharp
            if (ListUtil.IsEmpty(hocSinhIds))
            {
                // Trả về dictionary rỗng thay vì null: mọi nơi gọi đều dùng ContainsKey/indexer ngay.
                return new Dictionary<int, hocsinh_lop>();
            }

            Dictionary<int, hocsinh_lop> hocSinhIdsToHocSinhLops = new Dictionary<int, hocsinh_lop>();
```

**Check.** Build (two projects). Restored DB: `UPDATE bangtinhphi SET KhoiId = 999999 WHERE BangTinhPhiId = <any>;`
→ open Bảng tính phí → the grid paints with an empty Khối cell instead of crashing. Restore the row.

**Commit:** `Null-safe static lookups and StaticDataMap.Get`

---

## T13 — Google Drive backup: clearer failure, path independent of working dir

**Difficulty:** easy. **File:** `QLMamNon/Facade/GDriveFacade.cs` (**do not open `client_secret.json`**)

**Problems — Low-Medium.** (a) `request.Upload()` returns a status instead of throwing; on failure
`request.ResponseBody` is null and the code throws a bare `NullReferenceException`, so the backup
screen shows a meaningless message and the real cause (network, auth) is lost. (b)
`client_secret.json` is opened relative to the **current directory**; launching from a shortcut whose
"Start in" differs from the install folder breaks backup. The csproj already copies the file to the
output folder (`CopyToOutputDirectory=Always`). (c) The backup file is opened read-write although
only read.

**Step 1.** Find:
```csharp
using Google.Apis.Services;
```
Replace with:
```csharp
using Google.Apis.Services;
using Google.Apis.Upload;
```

**Step 2.** Find:
```csharp
            using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
```
Replace with:
```csharp
            // Đường dẫn tuyệt đối theo thư mục cài đặt, không phụ thuộc thư mục làm việc của shortcut.
            string clientSecretPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "client_secret.json");
            using (var stream = new FileStream(clientSecretPath, FileMode.Open, FileAccess.Read))
```

**Step 3.** Find:
```csharp
            using (var stream = new FileStream(fileToUpload, FileMode.Open))
            {
                request = service.Files.Create(fileMetadata, stream, "text/plain");
                request.Fields = "id";
                var uploadResult = request.Upload();
            }
```
Replace with:
```csharp
            using (var stream = new FileStream(fileToUpload, FileMode.Open, FileAccess.Read))
            {
                request = service.Files.Create(fileMetadata, stream, "text/plain");
                request.Fields = "id";
                IUploadProgress uploadResult = request.Upload();

                if (uploadResult.Status != UploadStatus.Completed)
                {
                    throw new IOException("Tải file sao lưu lên Google Drive không thành công.", uploadResult.Exception);
                }
            }
```

**Check.** Build. With the network unplugged, run Sao lưu dữ liệu → the error box says "Tải file sao
lưu lên Google Drive không thành công" and the log contains the inner network exception. With network
→ backup succeeds as before.

**Commit:** `GDrive backup: report upload failures and resolve client secret from the install folder`

---

## T14 — Latent crash traps in popup / activity / mediator code

**Difficulty:** easy. **Files:** `QLMamNon/Workflow/Handler/AdjustPopupEditFormAction.cs`,
`QLMamNon/Components/ActivityTracker/LoggingObserver.cs`, `ACG.Core.WinForm.Mediator/ModuleMediator.cs`

**Problems — Low.**
- `AdjustPopupEditFormAction.gvMain_ShowingPopupEditForm` uses `return` (not `continue`) when a popup
  button's caption is not "Update"/"Cancel", which silently skips wiring "cancel on close"; it also
  dereferences `CustomEditFormLayout as UCCRUDBase` without a null check, and `EditForm_FormClosed`
  does not check `uc.GridView`.
- `LoggingObserver.fire(string, ActivityType)` throws `NotImplementedException`; the base
  `ButtonHandler` calls exactly that overload.
- `ModuleMediator.Send` throws `KeyNotFoundException` for a channel nobody subscribed to.

**Step 1.** In `AdjustPopupEditFormAction.cs` find:
```csharp
                                    if (!EDIT_FORM_BUTTON_CAPTION_TO_IMAGE_MAP.ContainsKey(simpleButton.Text))
                                    {
                                        return;
                                    }
```
Replace with:
```csharp
                                    if (!EDIT_FORM_BUTTON_CAPTION_TO_IMAGE_MAP.ContainsKey(simpleButton.Text))
                                    {
                                        continue;
                                    }
```

**Step 2.** Same file, find:
```csharp
            GridView gridView = sender as GridView;
            UCCRUDBase uc = gridView.OptionsEditForm.CustomEditFormLayout as UCCRUDBase;
            uc.GridView = gridView;
            e.EditForm.FormClosed += new FormClosedEventHandler(EditForm_FormClosed);
```
Replace with:
```csharp
            GridView gridView = sender as GridView;
            UCCRUDBase uc = gridView == null ? null : gridView.OptionsEditForm.CustomEditFormLayout as UCCRUDBase;

            if (uc != null)
            {
                uc.GridView = gridView;
            }

            e.EditForm.FormClosed += new FormClosedEventHandler(EditForm_FormClosed);
```

**Step 3.** Same file, find:
```csharp
                    if (uc != null)
                    {
                        uc.GridView.CancelUpdateCurrentRow();
                    }
```
Replace with:
```csharp
                    if (uc != null && uc.GridView != null)
                    {
                        uc.GridView.CancelUpdateCurrentRow();
                    }
```

**Step 4.** In `LoggingObserver.cs` find:
```csharp
        public void fire(string objectId, ActivityType activityType)
        {
            throw new NotImplementedException();
        }
```
Replace with:
```csharp
        public void fire(string objectId, ActivityType activityType)
        {
            // Observer này chỉ ghi log khi đổi nội dung; các sự kiện khác được bỏ qua (không throw).
        }
```

**Step 5.** In `ModuleMediator.cs` find:
```csharp
        public void Send(object from, string channel, object message)
        {
            foreach (Action participant in participants[channel])
```
Replace with:
```csharp
        public void Send(object from, string channel, object message)
        {
            if (!participants.ContainsKey(channel))
            {
                return;
            }

            foreach (Action participant in participants[channel])
```

**Check.** Build (three projects). Open any danh mục form (e.g. Khối học), Thêm → popup shows "Lưu"/
"Hủy" with icons; close it with the window X → the new row is cancelled as before.

**Commit:** `Remove latent crash traps in popup, activity logging and mediator`

---

## T15 — Undisposed DbContexts

**Difficulty:** easy. **Files:** `QLMamNon/Service/Data/PhieuThuService.cs`,
`QLMamNon/Service/Data/SoThuTienService.cs`

**Problem — Low.** `PhieuThuService.InsertPhieuThu`, `UpdatePhieuThu` and
`SoThuTienService.GetDanhSachTreChuaNopHocPhiByMonth` create a `qlmamnonEntities` and never dispose it
(open MySQL connections linger until GC).

**Step 1.** In `PhieuThuService.cs` find:
```csharp
            qlmamnonEntities entities = StaticDataFacade.GetQLMNEntities();
            phieuthu phieuThu = new phieuthu()
            {
                Ngay = ngay,
                SoTien = soTienChuyenKhoan > 0 ? soTienChuyenKhoan : soTien,
                PaymentType = (soTienChuyenKhoan > 0 ? PaymentType.TRANSFER : PaymentType.CASH).ToString(),
                MaPhieu = maPhieu,
                GhiChu = ghiChu,
                HocSinhId = hocSinhId,
                CreatedDate = DateTime.Now,
                PhanLoaiThuId = phanLoaiThuId
            };
            entities.phieuthus.Add(phieuThu);
            entities.SaveChanges();
        }
```
Replace with:
```csharp
            using (qlmamnonEntities entities = StaticDataFacade.GetQLMNEntities())
            {
                phieuthu phieuThu = new phieuthu()
                {
                    Ngay = ngay,
                    SoTien = soTienChuyenKhoan > 0 ? soTienChuyenKhoan : soTien,
                    PaymentType = (soTienChuyenKhoan > 0 ? PaymentType.TRANSFER : PaymentType.CASH).ToString(),
                    MaPhieu = maPhieu,
                    GhiChu = ghiChu,
                    HocSinhId = hocSinhId,
                    CreatedDate = DateTime.Now,
                    PhanLoaiThuId = phanLoaiThuId
                };
                entities.phieuthus.Add(phieuThu);
                entities.SaveChanges();
            }
        }
```

**Step 2.** Same file, find:
```csharp
            qlmamnonEntities entities = StaticDataFacade.GetQLMNEntities();
            phieuthu phieuThu = entities.phieuthus.FirstOrDefault(p => p.PhieuThuId == phieuThuRow.PhieuThuId);

            if (phieuThu == null)
            {
                // Phiếu thu đã bị xóa ở nơi khác.
                throw new InvalidOperationException("Phiếu thu này không còn tồn tại. Vui lòng tải lại danh sách.");
            }

            phieuThu.SoTien = soTienChuyenKhoan > 0 ? soTienChuyenKhoan : soTien;
            phieuThu.PaymentType = (soTienChuyenKhoan > 0 ? PaymentType.TRANSFER : PaymentType.CASH).ToString();
            phieuThu.MaPhieu = maPhieu;
            phieuThu.GhiChu = ghiChu;
            phieuThu.HocSinhId = hocSinhId;
            phieuThu.PhanLoaiThuId = phanLoaiThuId;
            phieuThu.Ngay = ngay;
            entities.SaveChanges();
        }
```
Replace with:
```csharp
            using (qlmamnonEntities entities = StaticDataFacade.GetQLMNEntities())
            {
                phieuthu phieuThu = entities.phieuthus.FirstOrDefault(p => p.PhieuThuId == phieuThuRow.PhieuThuId);

                if (phieuThu == null)
                {
                    // Phiếu thu đã bị xóa ở nơi khác.
                    throw new InvalidOperationException("Phiếu thu này không còn tồn tại. Vui lòng tải lại danh sách.");
                }

                phieuThu.SoTien = soTienChuyenKhoan > 0 ? soTienChuyenKhoan : soTien;
                phieuThu.PaymentType = (soTienChuyenKhoan > 0 ? PaymentType.TRANSFER : PaymentType.CASH).ToString();
                phieuThu.MaPhieu = maPhieu;
                phieuThu.GhiChu = ghiChu;
                phieuThu.HocSinhId = hocSinhId;
                phieuThu.PhanLoaiThuId = phanLoaiThuId;
                phieuThu.Ngay = ngay;
                entities.SaveChanges();
            }
        }
```

**Step 3.** In `SoThuTienService.cs` find:
```csharp
            qlmamnonEntities entities = StaticDataFacade.GetQLMNEntities();
            List<HocSinhChuaNopHocPhiItem> table = entities.Database.SqlQuery<HocSinhChuaNopHocPhiItem>($"CALL `getDanhSachTreChuaNopHocPhiByMonth`({year}, {month})", new object[] { }).ToList();
            return table;
```
Replace with:
```csharp
            using (qlmamnonEntities entities = StaticDataFacade.GetQLMNEntities())
            {
                List<HocSinhChuaNopHocPhiItem> table = entities.Database.SqlQuery<HocSinhChuaNopHocPhiItem>($"CALL `getDanhSachTreChuaNopHocPhiByMonth`({year}, {month})", new object[] { }).ToList();
                return table;
            }
```

**Check.** Build. Create a phiếu thu, edit it, open Danh sách trẻ chưa nộp học phí → all work as before.

**Commit:** `Dispose short-lived DbContexts in PhieuThuService and SoThuTienService`

---

## T16 — Bảng kê thu học phí counts only tuition receipts

**Difficulty:** easy. **File:** `QLMamNon/Service/Data/SoThuTienService.cs`

**Problem — Medium (owner confirmed: the report must count tuition receipts only).**
`GetBangKeTongHopThuTien` (used by `FrmBangKeThuHocPhi`) takes **every** phiếu thu of the student in
the month as "Đã thu" against tuition, so a non-tuition receipt lowers "Còn nợ".
`GetBangKeTongHopThuTienForTaoPhieuThu` in the same file already filters on
`PhanLoaiThuConstant.PhanLoaiThuIdThuTienHocPhi`; apply the same filter here.

**Step 1.** Find:
```csharp
            foreach (phieuthu phieuThuRow in phieuThuDataTable)
            {
                viewbangthutien viewBangThuTienRow = hocSinhIdsToViewBangThuTienRows[phieuThuRow.HocSinhId.Value];
```
Replace with:
```csharp
            foreach (phieuthu phieuThuRow in phieuThuDataTable)
            {
                // Bảng kê thu học phí chỉ tính phiếu thu học phí (giống GetBangKeTongHopThuTienForTaoPhieuThu).
                if (!phieuThuRow.PhanLoaiThuId.HasValue || phieuThuRow.PhanLoaiThuId != PhanLoaiThuConstant.PhanLoaiThuIdThuTienHocPhi)
                {
                    continue;
                }

                viewbangthutien viewBangThuTienRow = hocSinhIdsToViewBangThuTienRows[phieuThuRow.HocSinhId.Value];
```
(`using QLMamNon.Constant;` is already at the top of the file. `FrmBangKeThuHocPhi` renumbers STT
itself, so nothing else changes.)

**Check.** Build. Restored DB: give a student one tuition receipt and one receipt of another category
in the same month → Bảng kê thu học phí for that month lists only the tuition receipt, and its Còn nợ
equals Phải thu − tuition paid.

**Commit:** `Bảng kê thu học phí: count tuition receipts only`

---

## T17 — Password hashing: salted PBKDF2 + automatic upgrade of old MD5 hashes

**Difficulty:** medium-hard — **use a stronger model or have the owner review the diff.**
**Files:** `ACG.Core.WinForm.Util/PasswordUtil.cs`, `QLMamNon/Service/Data/AuthenService.cs`,
`QLMamNon/Forms/Authenticate/FrmLogin.cs`, `QLMamNon/UserControls/UCEditFormUser.cs`

**Problem — Medium (security).** Passwords are stored as unsalted MD5 of `Encoding.Default` bytes
(the PC's ANSI code page). MD5 without salt is cracked instantly from a DB copy or backup, and a
password containing Vietnamese letters hashes differently on a PC with another code page, so that user
cannot log in there. The reference dump has 3 users, all 32-char MD5 hex.

**Decision (owner approved the migration).** No schema change and nobody is locked out:
- New format stored in the existing `Password varchar(255)` column:
  `pbkdf2$<iterations>$<salt base64>$<hash base64>` (PBKDF2, 10 000 iterations, 16-byte salt,
  32-byte hash, password as UTF-8). About 82 characters.
- Login reads the user by name and verifies in C#. A stored value **without** the `pbkdf2$` prefix is
  treated as the old MD5 and checked the old way; if it matches, it is **re-hashed in the new format
  and saved immediately**. Each user is migrated by their next successful login.
- Saving a password in the Người dùng screen always writes the new format. An empty password box
  leaves the stored password unchanged.
- The `getUser` stored procedure and `AuthenService.GetUsersForLogin` stay in place, unused (no DB
  migration script needed).

**Step 1.** In `PasswordUtil.cs` find (the end of `VerifyMd5Hash`):
```csharp
            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
```
Replace with:
```csharp
            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private const string Pbkdf2Prefix = "pbkdf2$";
        private const int Pbkdf2Iterations = 10000;
        private const int Pbkdf2SaltSize = 16;
        private const int Pbkdf2HashSize = 32;

        /// <summary>
        /// Băm mật khẩu bằng PBKDF2 có salt ngẫu nhiên, mật khẩu mã hóa UTF-8.
        /// Kết quả: "pbkdf2$&lt;số vòng&gt;$&lt;salt base64&gt;$&lt;hash base64&gt;".
        /// </summary>
        public static string HashPassword(string password)
        {
            byte[] salt = new byte[Pbkdf2SaltSize];

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            byte[] hash = pbkdf2(password, salt, Pbkdf2Iterations, Pbkdf2HashSize);

            return string.Format("{0}{1}${2}${3}", Pbkdf2Prefix, Pbkdf2Iterations,
                Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }

        /// <summary>
        /// true nếu giá trị đang lưu là mật khẩu kiểu cũ (MD5, chưa có tiền tố "pbkdf2$").
        /// </summary>
        public static bool IsLegacyMd5Hash(string storedHash)
        {
            return storedHash != null && !storedHash.StartsWith(Pbkdf2Prefix, StringComparison.Ordinal);
        }

        /// <summary>
        /// Kiểm tra mật khẩu với giá trị đang lưu, hỗ trợ cả kiểu mới (PBKDF2) và kiểu cũ (MD5).
        /// </summary>
        public static bool VerifyPassword(string password, string storedHash)
        {
            if (password == null || StringUtil.IsEmpty(storedHash))
            {
                return false;
            }

            if (IsLegacyMd5Hash(storedHash))
            {
                // Mật khẩu cũ: MD5 với Encoding.Default, giữ nguyên cách tính để người dùng cũ vẫn đăng nhập được.
                return VerifyMd5Hash(password, storedHash);
            }

            string[] parts = storedHash.Split('$');

            if (parts.Length != 4)
            {
                return false;
            }

            int iterations;

            if (!int.TryParse(parts[1], out iterations) || iterations <= 0)
            {
                return false;
            }

            byte[] salt;
            byte[] expectedHash;

            try
            {
                salt = Convert.FromBase64String(parts[2]);
                expectedHash = Convert.FromBase64String(parts[3]);
            }
            catch (FormatException)
            {
                return false;
            }

            if (expectedHash.Length == 0)
            {
                return false;
            }

            byte[] actualHash = pbkdf2(password, salt, iterations, expectedHash.Length);

            return fixedTimeEquals(actualHash, expectedHash);
        }

        private static byte[] pbkdf2(string password, byte[] salt, int iterations, int size)
        {
            using (Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(password), salt, iterations))
            {
                return deriveBytes.GetBytes(size);
            }
        }

        // So sánh không dừng sớm, để thời gian so sánh không lộ số byte trùng nhau.
        private static bool fixedTimeEquals(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
            {
                return false;
            }

            int diff = 0;

            for (int i = 0; i < a.Length; i++)
            {
                diff |= a[i] ^ b[i];
            }

            return diff == 0;
        }
```
(The usings `System`, `System.Security.Cryptography`, `System.Text` are already there. This project
targets .NET 4.0 — every API above exists there; do not switch to `Rfc2898DeriveBytes` overloads that
take a `HashAlgorithmName`, they need .NET 4.7.2.)

**Step 2.** In `AuthenService.cs` find:
```csharp
        public List<user> GetUsersForLogin(qlmamnonEntities entities, string userName, string password)
        {
            List<user> table = entities.getUser(userName, password).ToList();
            return table;
        }
```
Replace with:
```csharp
        public List<user> GetUsersForLogin(qlmamnonEntities entities, string userName, string password)
        {
            List<user> table = entities.getUser(userName, password).ToList();
            return table;
        }

        /// <summary>
        /// Tìm người dùng theo tên đăng nhập và kiểm tra mật khẩu (kiểu mới PBKDF2 hoặc kiểu cũ MD5).
        /// Mật khẩu kiểu cũ được băm lại theo kiểu mới ngay khi đăng nhập đúng. Trả về null nếu sai.
        /// </summary>
        public user Login(qlmamnonEntities entities, string userName, string password)
        {
            // So sánh UserName chạy trong MySQL với collation utf8_unicode_ci của cột, giống thủ tục getUser cũ.
            List<user> candidates = entities.users.Where(u => u.UserName == userName).ToList();

            foreach (user candidate in candidates)
            {
                if (!PasswordUtil.VerifyPassword(password, candidate.Password))
                {
                    continue;
                }

                if (PasswordUtil.IsLegacyMd5Hash(candidate.Password))
                {
                    try
                    {
                        candidate.Password = PasswordUtil.HashPassword(password);
                        entities.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        // Không chuyển được sang kiểu mới thì vẫn cho đăng nhập; lần sau sẽ thử lại.
                        LoggerFacade.Error(ex, AppForms.FormLogin);
                        entities.Entry(candidate).Reload();
                    }
                }

                return candidate;
            }

            return null;
        }
```
(`System`, `System.Linq`, `QLMamNon.Constant`, `QLMamNon.Facade`, `ACG.Core.WinForm.Util` are already
imported in this file.)

**Step 3.** In `FrmLogin.cs` find:
```csharp
            string password = PasswordUtil.GetMd5Hash(txtPassword.Text);
            List<user> userDataTable = authenService.GetUsersForLogin(entities, username, password);

            if (userDataTable.Count == 0)
```
Replace with:
```csharp
            user loginUser = authenService.Login(entities, username, txtPassword.Text);

            if (loginUser == null)
```
Then, in the same method, find:
```csharp
                List<user_privilege> upTable = authenService.LoadUserPrivileges(entities, userDataTable[0].UserId);
                authenService.SetAuthenticatedUser(userDataTable[0], upTable);
```
Replace with:
```csharp
                List<user_privilege> upTable = authenService.LoadUserPrivileges(entities, loginUser.UserId);
                authenService.SetAuthenticatedUser(loginUser, upTable);
```

**Step 4.** In `UCEditFormUser.cs` find:
```csharp
            this.GridView.SetFocusedRowCellValue("Password", PasswordUtil.GetMd5Hash(txtPassword.Text));
```
Replace with:
```csharp
            // Ô mật khẩu trống nghĩa là giữ nguyên mật khẩu đang lưu.
            if (StringUtil.IsEmpty(txtPassword.Text))
            {
                return;
            }

            this.GridView.SetFocusedRowCellValue("Password", PasswordUtil.HashPassword(txtPassword.Text));
```

**Check.** Build (both projects). Restored DB, with a known password for one test user (on the test
DB you may set one: `UPDATE user SET Password = MD5('Test123') WHERE UserName = '<user>';` — MySQL's
MD5 equals the old C# hash for ASCII passwords):
1. Log in with that user → succeeds. `SELECT UserName, LEFT(Password, 7) FROM user;` → that user now
   starts with `pbkdf2$`, the others are unchanged.
2. Restart the app, log in again → succeeds. Wrong password → "Tên đăng nhập hoặc Mật khẩu không
   đúng".
3. In the Người dùng screen set a new password for another user, Lưu, log in with it → succeeds, and
   its stored value starts with `pbkdf2$`. Edit a user without typing a password → stored value
   unchanged.

**Rollback note for the owner (put it in the commit body).** Once a user has logged in with this
build, an older build of the app can no longer log that user in. Recovery from MySQL:
`UPDATE user SET Password = MD5('<temporary password>') WHERE UserName = '<user>';` — the new build
accepts it and upgrades it on the next login.

**Commit:** `Hash passwords with salted PBKDF2 and upgrade legacy MD5 hashes at login`

---

## Owner decisions (2026-10-16)

1. Bảng kê thu học phí must count tuition receipts only → scheduled as **T16**.
2. SMS Unicode threshold (Latin-1-only accents sent as 7-bit) → **ignore**, not scheduled.
3. Password hashing migration → approved, scheduled as **T17**.

## Found and deliberately left alone

- SMS text with Latin-1-only accents (`á`, `ô`, …) is sent as 7-bit and arrives garbled
  (`USB3GSms.containsUnicodeCharacter` treats only chars > 255 as Unicode). **Owner: ignore.**
- `Service/SMS/ESms.cs` and `SpeedSMS.cs` are **not used** (only `USB3GSms` is instantiated). `ESms`
  would drop the last partial batch of numbers and send one content to a whole batch; `SpeedSMS.SendSMS`
  returns `null`. Fix only if they are ever wired up.
- `BangThuTienUtil.evaluateValuesForTienNop` resets amounts but not `NgayNopLan1/2`; both are
  `[NotMapped]` and freshly null on every load, so there is no visible effect today.
- `ActivityTrackerFacade.InitActivityTracker` adds a new `LoggingObserver` and re-runs
  `initControlHandlers()` on every form/popup open (slow growth of handler lists over a long session).
  Not a correctness bug; leave unless profiling shows it matters.
- Items already listed as out of scope in the two earlier plans stay out of scope.
