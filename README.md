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
🏗️ Kiến trúc & Design Patterns (Architecture Highlights)
Dự án không sử dụng code theo lối mòn (Spaghetti code) mà tuân thủ các nguyên tắc SOLID và áp dụng các Design Patterns sau:

1. Finite State Machine (FSM) - Quản lý luồng Game
Thay vì sử dụng hàng loạt biến bool hoặc switch-case khổng lồ trong Update(), dự án sử dụng FSM để quản lý trạng thái game một cách độc lập.

Implementation:

StateMachine.cs: Class quản lý việc chuyển đổi trạng thái.

BaseGameState.cs: Abstract class định nghĩa các phương thức Enter(), Exit(), UpdateState().

Các trạng thái cụ thể: MainMenuGameState, MainPlayGameState, MergeGameState, UseBoosterGameState...

Lợi ích: Tách biệt hoàn toàn logic điều khiển. Ví dụ: Khi đang ở trạng thái MergeGameState, người chơi không thể thực hiện thao tác Input của trạng thái MainPlayGameState.

2. Strategy Pattern - Hệ thống Vật phẩm (Boosters)
Hệ thống Booster được thiết kế để tuân thủ nguyên tắc Open/Closed Principle (SOLID) - Dễ dàng thêm vật phẩm mới mà không sửa code cũ.

Implementation:

IBoostLogic (Interface): Định nghĩa hành vi chung của mọi booster.

Concrete Strategies: NormalRocketLogic, SuperRocketLogic, ReverseLogic, EmptyBoosterLogic.

BoosterController: Context class nhận vào một IBoostLogic và thực thi nó mà không cần biết chi tiết bên trong.

Lợi ích: Nếu muốn thêm một loại búa đập đá, chỉ cần tạo class mới HammerLogic kế thừa IBoostLogic mà không ảnh hưởng đến code xử lý Input hay UI.

3. Object Pooling Pattern - Tối ưu hiệu năng
Game yêu cầu sinh ra và hủy bỏ liên tục các chồng gạch (HexaStack) và các hiệu ứng. Việc dùng Instantiate/Destroy liên tục sẽ gây ra Garbage Collection (GC) spike.

Implementation:

BaseObjectPool.cs: Generic class quản lý việc tái sử dụng đối tượng.

StackSpawner.cs: Sử dụng Pool để lấy các Stack thay vì tạo mới.

Lợi ích: Giữ FPS ổn định, giảm thiểu phân mảnh bộ nhớ.

4. Observer Pattern - Giao tiếp Decoupling
Sử dụng C# Action/Event để giao tiếp giữa các module mà không phụ thuộc vòng (Circular Dependency).

Implementation:

GameContext / ObserverManager: Nơi trung chuyển các sự kiện.

Ví dụ: Khi GridController phát hiện hết lượt đi -> Bắn sự kiện OnLevelFailed -> UIManager lắng nghe để hiện Popup thua, GameManager lắng nghe để dừng game.

Lợi ích: Module UI và Module Gameplay hoạt động độc lập.

5. Singleton Pattern
Sử dụng hạn chế và có kiểm soát cho các Manager duy nhất.

Implementation: Singleton.cs (Generic Singleton) áp dụng cho GameManager, UIManager, AudioManager.

Lợi ích: Dễ dàng truy cập các hệ thống cốt lõi từ bất kỳ đâu.

6. Data-Driven Design (ScriptableObjects)
Toàn bộ dữ liệu cấu hình game được tách biệt khỏi logic code.

Implementation:

LevelSO: Cấu hình thông số level (số lượt đi, mục tiêu, hình dáng map).

BoosterSO: Cấu hình thông tin hiển thị và logic của vật phẩm.

Lợi ích: Game Designer có thể cân bằng game, tạo level mới ngay trên Inspector mà không cần chạm vào code.

🧠 Logic Thuật toán (Algorithm)
Hệ tọa độ lục giác (Hexagonal Grid Logic)
Dự án sử dụng hệ tọa độ Axial Coordinates (q, r) để xử lý logic trên lưới lục giác thay vì tọa độ Descartes (x, y) thông thường.

Core Class: HexaAlgorithm.cs

Tính năng:

Tìm kiếm láng giềng (Neighbors check).

Tính toán khoảng cách và đường đi.

Xử lý logic hợp nhất (Merge) đệ quy: Kiểm tra các ô xung quanh -> Hợp nhất -> Tiếp tục kiểm tra lan truyền.
