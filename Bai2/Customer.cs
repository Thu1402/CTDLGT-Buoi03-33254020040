using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CTDLGT_Buoi03_33254020040.Bai2
{
    public class Customer
    {

        //khai báo đối tượng id kiểu int
        public int Id;

        // khai báo thời điểm khách đến kiểu long
        public long Arrival;

        // khai báo thời gian phục vụ khách kiểu long
        public long Service;

        // Khởi tạo thông tin cho một khách hàng.
        public Customer(int id, long arrival, long service)
        {
            Id = id;
            Arrival = arrival;
            Service = service;
        }
    }

    class Program
    {

        //viết hàm cho kiểm thử các ca từ 1 đến 5
        static void Main44()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            //đọc mảng gồm các chuỗi văn bản nhập từ bàn phím
            string[] input = Console.ReadLine()!.Split(' ');

            //nhập n với số khách từ bàn phím
            int n = int.Parse(input[0]);

            //nhập k số quầy từ bàn phím
            int k = int.Parse(input[1]);

            // khởi tạo hàng đợi biến queue để lưu khách theo đúng thứ tự đến.
            var queue = new Queue<Customer>();

            //vòng lặp duyệt từng đối tượng khách hàng từ 1 đến n, với biến i được định danh là ID để phân biệt các đối tượng khách hàng
            for (int i = 1; i <= n; i++)
            {
                //đọc dữ liệu nhập từ bàn phím thời điểm đến và thời gian đợi phục vụ của khách hàng
                string[] data = Console.ReadLine()!.Split(' ');

                //lưu thời điểm khách đến, lấy phần tử đầu tiên của mảng data[0] chuyển đổi kiểu dữ liệu từ string sang long và lưu vào biến a
                long a = long.Parse(data[0]);

                //lưu thời gian đợi chờ phục vụ, lấy phần tử thứ 2 của mảng data[1] chuyển đổi sang kiểu dữ liệu long và lưu vào biến s
                long s = long.Parse(data[1]);

                //khởi tạo đối tượng khách hàng mới với i là mã khách hàng, a là thời điểm đến, s là thời gian đợi phục vụ. Đưa khách vào cuối hàng đợi của queue
                queue.Enqueue(new Customer(i, a, s));
            }

            //khởi tạo mảng kiểu long với k kích thước tương ứng với số lượng quầy phục vụ, theo dõi thời gian rảnh của các quầy, ban đầu đều bằng 0.
            long[] free = new long[k];

            //khởi tạo tổng thời gian chờ, dùng kiểu long vì thời gian chờ có thể lớn, cộng dồn tổng thời gian chờ đợi của tất cả khách hàng
            long totalWait = 0;

            //ghi nhận thời gian chờ lâu nhất của khách hàng
            long maxWait = 0;

            //ghi nhận thời điểm phục vụ xong khách hàng
            long finishTime = 0;

            //vòng lặp xử lý đến khi không còn khách trong hàng đợi
            while (queue.Count > 0)
            {
                //lấy ra và xóa khách hàng đầu tiên đang đứng ở đầu hàng đợi, đối tượng gán vào biến customer để chuẩn bị xử lý thời gian đến và thời gian đợi phục vụ
                Customer customer = queue.Dequeue();

                //khai báo biến q với kiểu int, giải định quầy đầu tiên là tốt nhất để so sánh, có chỉ số 0
                int q = 0;

                //vòng lặp duyệt các quầy còn lại tìm quầy rảnh sớm nhất.
                for (int j = 1; j < k; j++)
                {
                    //so sánh mốc thời gian của quầy hiện tại j với mốc thời gian của quầy tạm thời q, nếu quầy j rảnh hơn thì gán q=j, nếu bằng nhau giữ quầy nhỏ.
                    if (free[j] < free[q])
                        q = j;
                }

                //lấy giá trị lớn nhất giưa 2 mốc thời gian, chỉ phục vụ khi cả khách đã đến và quầy đã rảnh
                long start = Math.Max(customer.Arrival, free[q]);

                //tính thời gian khách đứng chờ bằng lúc bắt đầu trừ lúc đến
                long wait = start - customer.Arrival;

                //thời điểm xong bằng lúc bắt đầu cộng thời gian phục vụ.
                long end = start + customer.Service;

                //cập nhật thời điểm quầy vừa chọn sẽ rảnh, để các lượt phân bổ sau biết rằng quầy này bận đến thời điểm đó
                free[q] = end;

                //cộng thời gian chờ của khách vào tổng.
                totalWait += wait;

                //cập nhật thời gian chờ lâu nhất.
                maxWait = Math.Max(maxWait, wait);

                //cập nhật thời điểm hoàn thành muộn nhất trong các quầy.
                finishTime = Math.Max(finishTime, end);

                //chỉ số mảng từ 0 nên cộng 1 để in số quầy từ 1.
                Console.WriteLine(
                    $"KH {customer.Id}: đến {customer.Arrival}, " +
                    $"quầy {q + 1}, bắt đầu {start}, " +
                    $"chờ {wait}, xong {end}");
            }

            //ép sang double để phép chia giữ được phần thập phân.
            double average = (double)totalWait / n;

            //in 2 chữ số thập phân và dùng dấu chấm theo yêu cầu.
            Console.WriteLine("Thời gian chờ trung bình: " +
                average.ToString("F2", CultureInfo.InvariantCulture));

            //in thời gian chờ lớn nhất.
            Console.WriteLine($"Chờ lâu nhất: {maxWait}");

            //in thời điểm hoàn thành tất cả khách.
            Console.WriteLine($"Hoàn thành lúc: {finishTime}");
        }


        //viết hàm cho kiểm thử ca 6
        static void Main442()
        {
            int n = 100_000;
            int k = 10;

            var queue = new Queue<Customer>();

            // Khách thứ i đến lúc i - 1, thời gian phục vụ bằng 10.
            for (int i = 1; i <= n; i++)
                queue.Enqueue(new Customer(i, i - 1, 10));

            long[] free = new long[k];
            long totalWait = 0;
            long maxWait = 0;
            long finishTime = 0;

            // Đo thời gian xử lý mô phỏng.
            var sw = System.Diagnostics.Stopwatch.StartNew();

            while (queue.Count > 0)
            {
                Customer customer = queue.Dequeue();

                int q = 0;
                for (int j = 1; j < k; j++)
                {
                    if (free[j] < free[q])
                        q = j;
                }

                long start = Math.Max(customer.Arrival, free[q]);
                long wait = start - customer.Arrival;
                long end = start + customer.Service;

                free[q] = end;
                totalWait += wait;
                maxWait = Math.Max(maxWait, wait);
                finishTime = Math.Max(finishTime, end);
            }

            sw.Stop();

            Console.WriteLine("Cho trung binh: " +
                ((double)totalWait / n).ToString(
                    "F2", CultureInfo.InvariantCulture));

            Console.WriteLine($"Cho lau nhat: {maxWait}");
            Console.WriteLine($"Hoan thanh luc: {finishTime}");
            Console.WriteLine(
                $"Thoi gian mo phong: {sw.Elapsed.TotalMilliseconds:F3} ms");
        }
    }

}
