using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CTDLGT_Buoi03_33254020040.Bai3
{
    
    public class CircularQueue<T>
    {
        // Mảng lưu phần tử, dung lượng ban đầu bằng 4.
        private T[] items = new T[4];

        // Vị trí phần tử đầu hàng.
        private int front = 0;

        // Vị trí sẽ thêm phần tử tiếp theo.
        private int rear = 0;

        // Số phần tử hiện có trong hàng.
        private int count = 0;

        // Trả về số phần tử, không cho bên ngoài thay đổi.
        public int Count => count;

        // Trả về dung lượng của mảng hiện tại.
        public int Capacity => items.Length;

        // Thêm phần tử vào cuối hàng.
        public void Enqueue(T value)
        {
            // Khi mảng đầy, mở rộng trước khi thêm.
            if (count == Capacity)
                Grow();

            // Ghi phần tử mới vào vị trí cuối hàng.
            items[rear] = value;

            // Dịch rear sang ô tiếp theo, quay về 0 nếu hết mảng.
            rear = (rear + 1) % Capacity;

            // Tăng số phần tử hiện có.
            count++;
        }

        // Lấy và xóa phần tử đầu hàng.
        public T Dequeue()
        {
            // Không thể lấy phần tử khi hàng rỗng.
            if (count == 0)
                throw new InvalidOperationException("Hang doi rong.");

            // Giữ lại giá trị đầu hàng để trả về.
            T value = items[front];

            // Xóa giá trị cũ để tránh giữ tham chiếu không cần thiết.
            items[front] = default(T)!;

            // Dịch front sang ô tiếp theo, không dồn dữ liệu.
            front = (front + 1) % Capacity;

            // Giảm số phần tử hiện có.
            count--;

            // Trả về phần tử vừa lấy.
            return value;
        }

        // Xem phần tử đầu hàng nhưng không xóa.
        public T Peek()
        {
            // Hàng rỗng thì không có phần tử để xem.
            if (count == 0)
                throw new InvalidOperationException("Hang doi rong.");

            // Chỉ đọc phần tử đầu hàng, không đổi front hoặc count.
            return items[front];
        }

        // Mở rộng mảng khi hàng đầy.
        private void Grow()
        {
            // Tạo mảng mới có dung lượng gấp đôi.
            T[] newItems = new T[Capacity * 2];

            // Chép theo thứ tự FIFO, bắt đầu từ front của mảng cũ.
            for (int i = 0; i < count; i++)
                newItems[i] = items[(front + i) % Capacity];

            // Thay mảng cũ bằng mảng mới.
            items = newItems;

            // Phần tử đầu hàng hiện nằm tại ô 0.
            front = 0;

            // Ô thêm tiếp theo nằm ngay sau các phần tử đã chép.
            rear = count;
        }
    }
    class Program
    {
        static void Main()
        {
            // CA 1: Hàng rỗng.
            Console.WriteLine("CA 1");

            var q1 = new CircularQueue<int>();

            // In trạng thái ban đầu.
            Console.WriteLine($"Count: {q1.Count}, Capacity: {q1.Capacity}");

            // Bắt ngoại lệ để chương trình tiếp tục chạy các ca sau.
            try
            {
                q1.Dequeue();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Dequeue: {ex.GetType().Name}");
            }

            // Kiểm tra riêng Peek trên hàng rỗng.
            try
            {
                q1.Peek();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Peek: {ex.GetType().Name}");
            }

            // CA 2: Thêm vừa đủ dung lượng ban đầu.
            Console.WriteLine("\nCA 2");

            var q2 = new CircularQueue<char>();

            // Thêm lần lượt A, B, C, D.
            foreach (char c in "ABCD")
                q2.Enqueue(c);

            // Peek phải đọc được phần tử đầu hàng mà không xóa.
            Console.WriteLine($"Peek: {q2.Peek()}");
            Console.WriteLine($"Count: {q2.Count}, Capacity: {q2.Capacity}");

            // CA 3: Quay vòng chỉ số nhưng chưa mở rộng.
            Console.WriteLine("\nCA 3");

            var q3 = new CircularQueue<char>();

            foreach (char c in "ABCD")
                q3.Enqueue(c);

            // Lấy hai phần tử đầu, tạo chỗ trống ở đầu mảng.
            Console.WriteLine($"Lay ra truoc: {q3.Dequeue()}, {q3.Dequeue()}");

            // Hai phần tử mới sử dụng lại các ô trống đầu mảng.
            q3.Enqueue('E');
            q3.Enqueue('F');

            // Lấy các phần tử còn lại theo thứ tự FIFO.
            Console.Write("Lay ra tiep: ");
            while (q3.Count > 0)
                Console.Write(q3.Dequeue() + " ");

            Console.WriteLine($"\nCount: {q3.Count}, Capacity: {q3.Capacity}");

            // CA 4: Mở rộng khi front bằng 0.
            Console.WriteLine("\nCA 4");

            var q4 = new CircularQueue<int>();

            // Phần tử thứ 5 làm dung lượng tăng từ 4 lên 8.
            for (int i = 1; i <= 5; i++)
                q4.Enqueue(i);

            Console.Write("Lay ra: ");
            while (q4.Count > 0)
                Console.Write(q4.Dequeue() + " ");

            Console.WriteLine($"\nCount: {q4.Count}, Capacity: {q4.Capacity}");

            // CA 5: Mở rộng khi front khác 0.
            Console.WriteLine("\nCA 5");

            var q5 = new CircularQueue<char>();

            foreach (char c in "ABCD")
                q5.Enqueue(c);

            // Sau hai lần lấy, front nằm tại chỉ số 2.
            Console.WriteLine($"Lay ra truoc: {q5.Dequeue()}, {q5.Dequeue()}");

            // E và F làm hàng đầy; thêm G sẽ mở rộng.
            foreach (char c in "EFG")
                q5.Enqueue(c);

            Console.Write("Lay ra tiep: ");
            while (q5.Count > 0)
                Console.Write(q5.Dequeue() + " ");

            Console.WriteLine($"\nCount: {q5.Count}, Capacity: {q5.Capacity}");

            // CA 6: Thêm rồi lấy ngay, lặp một triệu lần.
            Console.WriteLine("\nCA 6");

            var q6 = new CircularQueue<int>();

            // Đếm số lần giá trị lấy ra khác giá trị vừa thêm.
            int errors6 = 0;

            for (int i = 0; i < 1_000_000; i++)
            {
                q6.Enqueue(i);

                if (q6.Dequeue() != i)
                    errors6++;
            }

            Console.WriteLine($"So lan sai: {errors6}");
            Console.WriteLine($"Count: {q6.Count}, Capacity: {q6.Capacity}");

            // CA 7: Thêm 100.000 phần tử rồi lấy hết và đo thời gian.
            Console.WriteLine("\nCA 7");

            var q7 = new CircularQueue<int>();

            // Đếm số phần tử được lấy ra sai thứ tự.
            int errors7 = 0;

            // Bắt đầu đo trước khi thêm các phần tử.
            var sw = Stopwatch.StartNew();

            for (int i = 0; i < 100_000; i++)
                q7.Enqueue(i);

            // Giá trị lấy ra phải lần lượt là 0 đến 99.999.
            for (int i = 0; i < 100_000; i++)
            {
                if (q7.Dequeue() != i)
                    errors7++;
            }

            // Dừng đo trước khi in kết quả ra console.
            sw.Stop();

            Console.WriteLine($"So phan tu sai thu tu: {errors7}");
            Console.WriteLine($"Count: {q7.Count}, Capacity: {q7.Capacity}");
            Console.WriteLine($"Thoi gian: {sw.Elapsed.TotalMilliseconds:F3} ms");
        }
    }
    }
