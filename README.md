[![Xem Demo](https://img.youtube.com/vi/ZNzJyR-MkWk/0.jpg)](https://www.youtube.com/watch?v=ZNzJyR-MkWk)

# HexaSort Clone - Unity Project
Dự án cá nhân mô phỏng lại cơ chế xếp chồng và hợp nhất trên lưới lục giác, tập trung vào việc áp dụng và các **Design Patterns** để xây dựng hệ thống có khả năng mở rộng và dễ bảo trì.

---

## 🛠️ Tech Stack

* **Engine:** Unity 2022.3 (LTS)
* **Ngôn ngữ:** C#
* **Cấu trúc dự án:** Finite State Machine (FSM), MVC (Model-View-Controller) separation.
* **Thư viện hỗ trợ:** DOTween (Animation), TextMeshPro.
* **Lưu trữ dữ liệu:** JSON Serialization.

---

## ⚙️ Các tính năng kỹ thuật nổi bật

### 1. Kiến trúc hệ thống (System Architecture)
* **Finite State Machine (FSM):** Quản lý luồng game thông qua `GameManager` và các trạng thái riêng biệt kế thừa từ `BaseGameState`.
    * Các trạng thái: `MainPlay`, `Merge`, `UseBooster`, `LevelBrief`, `LevelCompleted`, `LevelFailed`.
    * Tác dụng: Tách biệt hoàn toàn logic điều khiển (Input handling) của từng giai đoạn game, tránh điều kiện `if-else` lồng nhau.
* **Data-Driven Design:** Sử dụng `ScriptableObject` để cấu hình dữ liệu tĩnh:
    * `LevelSO`: Thiết lập kích thước Grid, mục tiêu (TargetGoal), giới hạn lượt đi (MoveLimit).
    * `BoosterSO`: Định nghĩa thông tin và loại vật phẩm hỗ trợ.

### 2. Thuật toán & Grid Logic
* **Hệ tọa độ Axial (Q, R, S):** Triển khai hệ thống tọa độ lục giác (Hexagonal Coordinate System) thay vì tọa độ Descartes để xử lý logic láng giềng chính xác hơn.
* **Recursive Algorithm:** Xây dựng `HexaAlgorithm.GetNeighborsInRadius` sử dụng đệ quy để tìm kiếm các ô lân cận và xử lý logic nổ (Blast) theo bán kính.
* **Merge Logic:** Xử lý logic hợp nhất chồng (Stack Merging) bất đồng bộ thông qua `Coroutine`, đảm bảo cập nhật visual mượt mà trước khi tính toán logic tiếp theo.

### 3. Design Patterns ứng dụng
* **Strategy Pattern:** Áp dụng cho hệ thống Booster thông qua interface `IBoostLogic`.
    * Dễ dàng thêm logic mới (`NormalRocketLogic`, `SuperRocketLogic`, `ReverseLogic`) mà không cần sửa đổi `BoosterController`.
* **Singleton Pattern:** Sử dụng cho các lớp quản lý duy nhất như `BillboardManager` để tối ưu hóa việc truy cập global.

### 4. Hệ thống bổ trợ
* **Save/Load System:** Lưu trữ tiến trình người chơi (Level, Inventory) xuống local file định dạng JSON thông qua `SaveSystem` generic.
* **UI Management:** Quản lý các màn hình UI (Panel) theo cơ chế đăng ký và chuyển đổi trạng thái tự động trong `UIManager`.

---

## 🚀 Định hướng phát triển (Future Improvements)

* Chuyển đổi hệ thống quản lý tài nguyên sang **Addressables** để tối ưu hóa việc tải assets.
* Tích hợp **Unit Tests** cho các thuật toán tính toán trên lưới (HexaAlgorithm).
* Nâng cấp Visual Effects bằng **Shader Graph**.
 
---
