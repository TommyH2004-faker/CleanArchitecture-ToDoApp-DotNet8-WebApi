# 🧪 HƯỚNG DẪN TEST API

## 📂 Cấu trúc file đã tạo/sửa:

### ✅ Files đã tạo mới:
```
TodoApp.Application/
  ├── DTOs/
  │   └── UserDto.cs                    # DTOs cho User (UserDto, CreateUserDto, UpdateUserDto)
  ├── Common/
  │   └── Result.cs                     # Result Pattern để xử lý kết quả
  └── Mappings/
      └── UserMappingExtensions.cs      # Extension methods để map Entity <-> DTO
```

### 🔧 Files đã sửa:
```
TodoApp.Domain/Entities/User.cs         # Thêm validation, factory method, private setters
TodoApp.Application/Interfaces/IUserService.cs  # Đổi sang dùng DTOs và Result
TodoApp.Application/Services/UserService.cs     # Implement business logic đầy đủ
TodoApp.WebAPI/Controllers/UserController.cs    # Update để dùng DTOs và Result Pattern
```

---

## 🚀 Bước 1: Build lại project

```powershell
cd d:\PJ_Architecture\CleanArchitecture-ToDoApp-DotNet8-WebApi\TodoApp.WebAPI
dotnet build
```

**Lưu ý:** Nếu có lỗi migration do thay đổi Entity, chạy:
```powershell
cd ..\TodoApp.Infrastructure
dotnet ef migrations add UpdateUserEntity --startup-project ..\TodoApp.WebAPI
dotnet ef database update --startup-project ..\TodoApp.WebAPI
```

---

## 🚀 Bước 2: Chạy API

```powershell
cd ..\TodoApp.WebAPI
dotnet run
```

Hoặc sử dụng Docker:
```powershell
docker-compose up
```

API sẽ chạy tại: `https://localhost:5001` hoặc `http://localhost:5000`

---

## 🧪 Bước 3: Test API

### **Option 1: Dùng Swagger UI**
1. Mở browser: `http://localhost:5000/swagger`
2. Test các endpoints trực tiếp trên UI

### **Option 2: Dùng PowerShell**

#### ✅ Test 1: GET All Users
```powershell
Invoke-RestMethod -Uri "http://localhost:5000/api/user" -Method Get | ConvertTo-Json
```

**Kết quả mong đợi:**
```json
[
  {
    "userId": 1,
    "username": "john_doe",
    "email": "john.doe@example.com",
    "createdAt": "2025-12-29T10:00:00Z"
  }
]
```

---

#### ✅ Test 2: GET User by ID
```powershell
Invoke-RestMethod -Uri "http://localhost:5000/api/user/1" -Method Get | ConvertTo-Json
```

**Test case thất bại (User không tồn tại):**
```powershell
Invoke-RestMethod -Uri "http://localhost:5000/api/user/999" -Method Get
# Kết quả: 404 Not Found + error message
```

---

#### ✅ Test 3: POST Create User (Success)
```powershell
$body = @{
    username = "alice_wonder"
    email = "alice@example.com"
} | ConvertTo-Json

Invoke-RestMethod -Uri "http://localhost:5000/api/user" `
    -Method Post `
    -Body $body `
    -ContentType "application/json" | ConvertTo-Json
```

**Kết quả mong đợi:**
```json
{
  "userId": 3,
  "username": "alice_wonder",
  "email": "alice@example.com",
  "createdAt": "2025-12-29T..."
}
```

---

#### ❌ Test 4: POST Create User (Validation Errors)

**Test case 1: Username quá ngắn**
```powershell
$body = @{
    username = "ab"
    email = "test@example.com"
} | ConvertTo-Json

Invoke-RestMethod -Uri "http://localhost:5000/api/user" `
    -Method Post `
    -Body $body `
    -ContentType "application/json"
# Kết quả: 400 Bad Request + "Username must be between 3 and 50 characters"
```

**Test case 2: Email invalid**
```powershell
$body = @{
    username = "testuser"
    email = "invalid-email"
} | ConvertTo-Json

Invoke-RestMethod -Uri "http://localhost:5000/api/user" `
    -Method Post `
    -Body $body `
    -ContentType "application/json"
# Kết quả: 400 Bad Request + "Invalid email format"
```

**Test case 3: Username đã tồn tại**
```powershell
$body = @{
    username = "john_doe"
    email = "another@example.com"
} | ConvertTo-Json

Invoke-RestMethod -Uri "http://localhost:5000/api/user" `
    -Method Post `
    -Body $body `
    -ContentType "application/json"
# Kết quả: 400 Bad Request + "Username already exists"
```

---

#### ✅ Test 5: PUT Update User
```powershell
$body = @{
    username = "john_updated"
    email = "john.updated@example.com"
} | ConvertTo-Json

Invoke-RestMethod -Uri "http://localhost:5000/api/user/1" `
    -Method Put `
    -Body $body `
    -ContentType "application/json" | ConvertTo-Json
```

---

#### ✅ Test 6: DELETE User
```powershell
Invoke-RestMethod -Uri "http://localhost:5000/api/user/1" -Method Delete
# Kết quả: 204 No Content
```

**Test xóa user không tồn tại:**
```powershell
Invoke-RestMethod -Uri "http://localhost:5000/api/user/999" -Method Delete
# Kết quả: 404 Not Found
```

---

## 📝 Test với React Frontend

Cập nhật file `UserApi.ts` trong React project:

```typescript
// src/Api/UserApi.ts
export interface User {
    userId: number;
    username: string;
    email: string;
    createdAt: string;
}

export interface CreateUserDto {
    username: string;
    email: string;
}

export interface UpdateUserDto {
    username: string;
    email: string;
}

const API_URL = "http://localhost:5000/api/user";

export const getAllUsers = async (): Promise<User[]> => {
    const response = await fetch(API_URL);
    if (!response.ok) throw new Error('Failed to fetch users');
    return response.json();
};

export const createUser = async (dto: CreateUserDto): Promise<User> => {
    const response = await fetch(API_URL, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(dto)
    });
    
    if (!response.ok) {
        const error = await response.json();
        throw new Error(error.error || error.errors?.join(', ') || 'Failed to create user');
    }
    
    return response.json();
};

export const updateUser = async (id: number, dto: UpdateUserDto): Promise<User> => {
    const response = await fetch(`${API_URL}/${id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(dto)
    });
    
    if (!response.ok) {
        const error = await response.json();
        throw new Error(error.error || error.errors?.join(', ') || 'Failed to update user');
    }
    
    return response.json();
};

export const deleteUser = async (id: number): Promise<void> => {
    const response = await fetch(`${API_URL}/${id}`, {
        method: 'DELETE'
    });
    
    if (!response.ok) {
        const error = await response.json();
        throw new Error(error.error || 'Failed to delete user');
    }
};
```

---

## ✅ Các điểm đã cải thiện:

1. ✅ **DTOs** - Tách biệt domain entities và API contracts
2. ✅ **Result Pattern** - Xử lý lỗi chuẩn, không throw exception lung tung
3. ✅ **Domain Validation** - Entity tự validate với factory method
4. ✅ **Business Logic** - Service layer có logic check duplicate, validation
5. ✅ **Proper HTTP Status Codes** - 200, 201, 204, 400, 404
6. ✅ **Error Messages** - Trả về message rõ ràng cho từng trường hợp
7. ✅ **Immutable Entities** - Private setters, chỉ update qua methods

---

## 🎯 Kết quả mong đợi:

- ✅ API trả về JSON chuẩn, không có navigation properties
- ✅ Validation errors trả về rõ ràng
- ✅ Không thể tạo user với username/email trùng
- ✅ Không thể tạo user với email/username invalid
- ✅ Status codes đúng chuẩn REST API
- ✅ React app có thể call API và hiển thị lỗi đúng
