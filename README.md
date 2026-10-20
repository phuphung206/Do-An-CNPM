# 🛡️ Secure Data Backup System

Hệ thống sao lưu và phục hồi dữ liệu tự động theo mô hình Client-Server. Đồ án được thiết kế để giải quyết bài toán đồng bộ dữ liệu an toàn qua mạng, quản lý luồng hệ thống (multithreading), và tối ưu hóa dung lượng lưu trữ thông qua cơ chế Incremental Backup.

##  Tính năng nổi bật
*   **Quản lý cấu hình: Giao diện cho phép người dùng chọn thư mục/file cần backup (hỗ trợ thao tác kéo thả, lọc theo định dạng file).
*   **Cơ chế Backup:
*   **Full Backup: Sao lưu toàn bộ dữ liệu định kỳ.
*   **Incremental Backup: Chỉ sao lưu những file có sự thay đổi (dựa vào thuật toán băm - Hash SHA/MD5 hoặc thời gian chỉnh sửa) kể từ lần backup gần nhất để tiết kiệm băng thông.
*   **Lên lịch tự động (Scheduler): Thiết lập các tác vụ chạy ngầm (cronjob) để tự động backup hàng ngày, hàng tuần.
*   **Quản lý phiên bản (Version Control) & Phục hồi (Restore): Lưu trữ danh sách các lần backup. Người dùng có thể xem lại lịch sử và chọn khôi phục toàn bộ thư mục hoặc một file đơn lẻ về đúng thời điểm mong muốn.
*   **Bảo mật & Mã hóa: Triển khai mã hóa dữ liệu (AES-256) ở phía Client trước khi truyền qua mạng. Đảm bảo dữ liệu dù có bị đánh cắp trên đường truyền hay trên Server thì cũng không thể đọc được nếu không có khóa giải mã của Client.
*   **Xử lý Đa luồng (Multithreading): Chạy tiến trình nén file và gửi qua mạng dưới nền (Background Worker), chia nhỏ các file dung lượng lớn (chunking) để gửi và hiển thị thanh tiến trình (progress bar) mà không làm treo giao diện UI.
*   **Chống Ransomware bằng AI: Tích hợp một module nhỏ xử lý bằng Python để phân tích độ bất thường (anomaly detection). Nếu phần mềm nhận thấy các file trong máy tính đột nhiên bị đổi đuôi hoặc mã hóa hàng loạt (dấu hiệu của virus tống tiền), nó sẽ chặn ngay lập tức việc backup để tránh ghi đè dữ liệu rác lên bản backup sạch trước đó trên Server.
  
### Phía Client (Ứng dụng người dùng)
*   **Incremental & Full Backup:** Tối ưu hóa băng thông bằng cách chỉ gửi đi những file có sự thay đổi (dựa trên mã băm SHA-256 và `LastModifiedTime`).
*   **Real-time Monitoring:** Tích hợp `FileSystemWatcher` để giám sát và phát hiện ngay lập tức các thay đổi thêm/sửa/xóa trong thư mục được cấu hình.
*   **Lên lịch tự động (Scheduler):** Cho phép thiết lập các tác vụ sao lưu chạy ngầm định kỳ.
*   **Quản lý phiên bản & Restore:** Xem lại lịch sử các bản backup và khôi phục (rollback) dữ liệu về đúng thời điểm mong muốn.

### Phía Server (Lưu trữ tập trung)
*   **Truyền tải mạng tin cậy:** Giao tiếp đa luồng qua **TCP Sockets**, hỗ trợ chia nhỏ gói tin (chunking) để nhận các file có dung lượng lớn mà không bị gián đoạn.
*   **Quản lý Metadata:** Lưu trữ thông tin người dùng, lịch sử phiên bản và ánh xạ đường dẫn vật lý trên cơ sở dữ liệu.

##  Kiến trúc & Công nghệ sử dụng

*   **Ngôn ngữ & Nền tảng:** C# / .NET Framework
*   **Giao diện (GUI):** Windows Forms / WPF
*   **Giao tiếp mạng:** TCP/UDP Sockets (Tự định nghĩa cấu trúc Packet Messages)
*   **Cơ sở dữ liệu Server:** SQLite / MySQL 
*   **Xử lý luồng:** Tận dụng `Task`, `Thread` và `BackgroundWorker` để xử lý I/O và truyền file không làm treo giao diện.

##  Hướng dẫn cài đặt (Installation)

### Yêu cầu hệ thống:
*   Visual Studio 2022 hoặc mới hơn.
*   .NET Framework 4.7.2 / .NET 6.0+
*   MySQL Server.
