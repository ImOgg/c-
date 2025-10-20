# API 專案

## 資料庫 Migration 紀錄

### AppUser Migration - 2025/10/20

#### Entity 定義
位置：`Entities/AppUser.cs`

```csharp
public class AppUser
{
    public int MyProperty { get; set; }
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string DisplayName { get; set; } = string.Empty;
    public required string Email { get; set; } = string.Empty;
}
```

#### Migration 執行步驟

1. **創建 Migration**
   ```bash
   dotnet ef migrations add InitialCreateAppUser
   ```
   - 建立時間：2025-10-20 09:03:13
   - Migration 檔案：`Migrations/20251020090313_InitialCreateAppUser.cs`

2. **套用 Migration 到資料庫**
   ```bash
   dotnet ef database update
   ```
   - 執行結果：成功
   - 資料庫：MySQL (使用 Pomelo.EntityFrameworkCore.MySql)

#### 資料表結構

**Users 資料表：**
| 欄位名稱 | 資料類型 | 說明 |
|---------|---------|------|
| Id | varchar(255) | 主鍵，使用 GUID |
| MyProperty | int | 整數屬性 |
| DisplayName | longtext | 顯示名稱（必填）|
| Email | longtext | Email 地址（必填）|

#### 變更內容
- 移除了之前的 `Products` 資料表
- 新增 `Users` 資料表
- 使用 UTF-8 MB4 字元集

---

## 如何使用 EF Core Migration

### 基本命令

**創建新的 Migration：**
```bash
dotnet ef migrations add <MigrationName>
```

**更新資料庫：**
```bash
dotnet ef database update
```

**查看所有 Migrations：**
```bash
dotnet ef migrations list
```

**移除最後一個 Migration（尚未套用到資料庫的情況）：**
```bash
dotnet ef migrations remove
```

**回滾到特定 Migration：**
```bash
dotnet ef database update <MigrationName>
```

**產生 SQL 腳本（不直接執行）：**
```bash
dotnet ef migrations script
```

### 工作流程

1. 修改 Entity 類別（如 `AppUser`）
2. 執行 `dotnet ef migrations add <描述性名稱>`
3. 檢查生成的 Migration 檔案
4. 執行 `dotnet ef database update` 套用變更
5. 確認資料庫變更成功

---

## 專案資訊

- **.NET 版本：** .NET 8.0
- **資料庫：** MySQL
- **ORM：** Entity Framework Core 9.0.10
- **MySQL Provider：** Pomelo.EntityFrameworkCore.MySql 9.0.0
