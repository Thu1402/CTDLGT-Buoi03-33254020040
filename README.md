MSSV: 33254020040
Họ Tên: Lê Thị Anh Thư
LHP: 26C1INF50900706
Bài 1: 
Mã code:
public class stack
{
   //Nhận chuỗi s và trả về kết quả cùng vị trí lỗi
    static string KiemTraNgoac(string s)
    {
        //lưu mỗi dấu ngoặc mở cùng vị trí và kiểm tra từng cặp và báo đúng chỗ lỗi nếu nó không được đóng
        var stack = new Stack<(char ch, int pos)>();

        //duyệt chuỗi từ vị trí từ trái sang phải bắt đầu từ vị trí 0
        for (int i = 0; i < s.Length; i++)
        {
            //lấy ký từ tại vị trí thứ i trong chuỗi s,luu giá trị ký tự đó vào biến c
            char c = s[i];

            // Ngoặc mở: kiểm tra ký tự c hiện tại có phải là 1 trong 3 dấu ngoặc mở đang xét
            if (c == '(' || c == '[' || c == '{')
                   
                //nếu 1 trong 3 điều kiện đúng thì sẽ đẩy c ký tự dấu ngoặc và i vị trí dấu ngoặc lên trên cùng stack
                stack.Push((c, i));

            // Ngoặc đóng: kiểm tra ký tự c hiện tại có phải là 1 trong 3 dấu ngoặc đóng đang xét với dấu ngoặc mở
            else if (c == ')' || c == ']' || c == '}')
            {
                //kiểm tra stack có phần tử nào trống hoặc thừa dấu ngoặc thì sẽ báo lỗi sai vị trí thứ i
                if (stack.Count == 0)
                    return $"SAI tai vi tri {i}";

                //lấy phần tử dấu ngoặc mở nằm trên cùng gần nhất của stack để đối chiếu so sánh với dấu đóng hiện tại 
                char mo = stack.Pop().ch;

                //điều kiện sẽ chạy nếu cặp ngoặc bên trong không khớp loại với nhau
                if (!((mo == '(' && c == ')') ||
                      (mo == '[' && c == ']') ||
                      (mo == '{' && c == '}')))

                    //sau khi phát hiện ra lỗi dấu cặp ngoặc không đúng, sẽ báo lỗi kèm theo vị trí thứ i
                    return $"SAI tai vi tri {i}";
            }
        }

        // Lấy vị trí ngoặc mở chưa đóng sớm nhất kiểm tra xem stack còn dấu ngoặc mở chưa đóng không
        if (stack.Count > 0)
        {
            //khởi tạo biến để lưu vị trí lỗi vào pos với giá trị mặc định -1 kiểm tra vòng lặp thực tế
            int pos = -1;

            //vòng lặp duyệt lần lượt các vị trí dấu mở chưa được đóng còn sót ra khỏi stack cho đến khi satck trống hoàn toàn và lưu vào biến pos
            while (stack.Count > 0)
                pos = stack.Pop().pos;

            //thông báo các vị trí lỗi trong biến pos
            return $"SAI tai vi tri {pos}";
        }
        //nếu không có dấu ngoặc mở nào chưa đúng thì thông báo HỢP LỆ
        return "HOP LE";
    }

    static void Main()
    {
        string s = Console.ReadLine() ?? "";
        Console.WriteLine(KiemTraNgoac(s));

        //tạo bảng test cho từng pha kiểm thử
        string[] tests =
            {"{(a+b)x[c-d]}",
            "{(a+b]xc)",
            "(a+b",
            "a+b)",
            "",
            "([)]",
            "a+b",
            "{[(",
            new string('(', 500_000) + new string(')', 500_000),
            new string('(', 1_000_000)
        };

        //duyệt từng mảng trong đoạn test
        for (int i = 0; i < tests.Length; i++)
        {
            //tại biến mới sư, sử dụng Stopmatch để đo thời gian chạy và độ chính xác cao của mỗi đoạn code và lưu vào biến sw
            var sw = System.Diagnostics.Stopwatch.StartNew();

            //gọi hàm để kiểm tra chuỗi test các vị trí thứ tự i và lưu kết quả trả về hợp lệ hay vị trí vào biến ketQua
            string ketQua = KiemTraNgoac(tests[i]);

            //dừng đồng hồ khi thực thi xong chương trình
            sw.Stop();

            //xuất ra chuỗi thông báo với i vị trí, kết quả hợp lệ hay sai vị trí thứ bao nhiêu kèm với thời gian tính bằng ms lấy từ biến sw
            Console.WriteLine(
                $"Ca {i + 1}: {ketQua} | " +
                $"{sw.Elapsed.TotalMilliseconds:F3} ms");
        }
    }

}
Kết quả:
 
Giải thích:
1.	Vì sao dùng stack phù hợp hơn là dùng Queue: vì dấu ngoặc có tính lồng nhau, cặp được đóng gần nhất là cặp được mở gần nhất. Stack sẽ có phép so ngoặc đóng hiện tại với ngoặc mở chưa đóng gần nhất. Queue sẽ lấy phần tử vào sớm nhất ra trước là ngoặc mở ngoài cùng, như vậy thì sẽ báo sai dù có đúng chuỗi, muốn sửa thì phải duyệt lại, sẽ tốn Θ(n).
2.	Ba loại lỗi được phát hiện ở mã dòng nào
-	Đóng sai loại: ngoặc đóng không khớp với ngoặc mở vừa lấy từ đỉnh stack.
-	  char mo = stack.Pop().ch;
-	
-	  //điều kiện sẽ chạy nếu cặp ngoặc bên trong không khớp loại với nhau
-	  if (!((mo == '(' && c == ')') ||
-	        (mo == '[' && c == ']') ||
-	        (mo == '{' && c == '}')))
-	
-	      //sau khi phát hiện ra lỗi dấu cặp ngoặc không đúng, sẽ báo lỗi kèm theo vị trí thứ i
-	      return $"SAI tai vi tri {i}";
-	Đóng thừa: gặp ngoặc đóng nhưng stack rỗng, không có ngoặc mở tương ứng
-	  if (stack.Count == 0)
-	      return $"SAI tai vi tri {i}";
-	Ngoặc mở chưa đóng: duyệt hết chuỗi những stack vẫn còn ngoặc mở
-	    if (stack.Count > 0)
-	    {
-	        //khởi tạo biến để lưu vị trí lỗi vào pos với giá trị mặc định -1 kiểm tra vòng lặp thực tế
-	        int pos = -1;
-	
-	        //vòng lặp duyệt lần lượt các vị trí dấu mở chưa được đóng còn sót ra khỏi stack cho đến khi satck trống hoàn toàn và lưu vào biến pos
-	        while (stack.Count > 0)
-	            pos = stack.Pop().pos;
-	
-	        //thông báo các vị trí lỗi trong biến pos
-	        return $"SAI tai vi tri {pos}";
-	    }
-	    //nếu không có dấu ngoặc mở nào chưa đúng thì thông báo HỢP LỆ
-	    return "HOP LE";

3.	Kiểm tra stack rỗng trước khi lấy phần tử ra, vì nếu gọi Pop() khi Stack rỗng thì chương trình sẽ phát sinh ra lỗi. Kiểm tra Stack rỗng if (stack.Count == 0) trước giúp tránh lỗi và phát hiện ra ngoặc đóng thừa.
4.	Lưu vị trí cùng ngoặc để kiểm tra đúng loại, để báo chính xác nơi xảy ra lõi theo sỗ bắt đầu từ 0. Với ngoặc mở chưa đóng, phải phát hiển ngoặc xuất hiện sớm nhất. Ngoặc này nằm ở cuối đoạn Stack, nên ta lấy hết các phần tử ra, vị trí lấy ra cuối cùng là vị trí cần thông báo.
Giải thích đoạn mã:
Độ phức tạp: trong trường hợp xấu nhất, thời gian là Θ(n) vì mỗi ký tự được xét một lần, mỗi ngoặc mở được Push một lần và Pop một lần. Bộ nhớ phụ O(n) vì stack có thể chứa toàn bộ b ký tự nếu chuỗi chỉ gồm ngoặc mở. Các ca gặp ngoặc đóng thừa hoặc sai loại có thể dừng sớm.
Chương trình sử dụng stack để kiểm tra các dấu ngoặc (), [], {} có được đóng mở đúng cặp và theo thứ tự hay không. Mỗi phần tử trong stack lưu ký tự ngoặc mở và vị trí của nó trong chuỗi. stack hoạt động theo nguyên tắc vào sau ra trước và phù hợp với tính chất ngoặc mở sau cùng phải đóng trước.
Ban đầu stack rỗng, và nạp vào chuỗi duyệt từ trái sang phải bằng lặp for thì nếu gặp ngoặc mở, chương trình dùng push để đưa ngoặc cùng vị trí vòa stack. Nếu gặp ký tự không đúng ngoặc thì bỏ qua.
Khi gặp ngoặc đóng, chương trình kiểm tra stack có rỗng không, nếu rỗng, ngoặc đóng không có ngoặc mở tương ứng, chương trình sẽ báo “Sai tại vị trí i”. Nếu tác không rỗng, chương trình dùng Pop lấy ngoặc mở trên đỉnh để so sánh, nếu hai ngoặc không cùng loại, chương trình cũng báo lỗi tại vị trí đóng ngoặc và ngừng lại.
Sau khi duyệt chuỗi xong, nếu stack vẫn còn phẩn tử ngoặc mở chưa đóng. Chương trình lần lượt Pop tất cả các phần tử trong stack, chính là ngoặc mở chưa đóng xuất hiện sớm nhất. vị trí ngoặc này được trả về báo lỗi. Nếu stack rỗng, chương trình trả về Hợp lệ.
Dùng Stopwatch để đo thời gian xử lý và in kết quả thực tế ở từng ca kiểm thử.

Bài 2: 
Mã code:
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
    static void Main33()
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
    static void Main()
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
Kết quả:
Ca 1: 
 
Ca 2:
 
Ca 3: 
 
Ca 4: 
 
Ca 5: 
 
Ca 6: 
 
Gải thích:
5.	Vì sao dùng queue không dùng stack, vì: queue hoạt động theo nguyên tắc vào trước ra trước, phù hợp với yêu cầu khách đến trước được phục vụ trước. stack hoạt động theo nguyên tắc vào sau ra trước như vậy sẽ sai quy trình phục vụ khách hàng.
6.	Cách chọn quầy rảnh sớm nhất và vì sao tốn Θ(k): mảng free[] dùng để lưu thười điểm rảnh từng quầy, lấy mốc từ quầy đầu tiên:
int q = 0;

//vòng lặp duyệt các quầy còn lại tìm quầy rảnh sớm nhất.
for (int j = 1; j < k; j++)
{
    //so sánh mốc thời gian của quầy hiện tại j với mốc thời gian của quầy tạm thời q, nếu quầy j rảnh hơn thì gán q=j, nếu bằng nhau giữ quầy nhỏ.
    if (free[j] < free[q])
        q = j;
}
Vì duyệt từ quầy nhỏ đến lớn nên sẽ chọn đúng quầy có số thứ tự nhỏ nhất. Mỗi khách cần xét đủ k quầy, gồm quầy ban đầu và k-1 lần so sánh, nên phải tốn Θ(k).
7.	Vì sao lấy giá trị lớn hơn giữa lúc quầy rảnh và lúc khách đến, vì: khách chỉ được phục vụ khi khách đến đến và quầy đang rảnh
long start = Math.Max(customer.Arrival, free[q]);
nếu khách đến lúc 2 quầy rảnh lúc 5 thì khách phải bắt đầu úc 5 và chờ 3
8.	Vì  sao dùng long cho tổng thời gian chờ, vì tổng thời gian chờ có thể vượt quá giới hạn kiểu int
Giải thích tổng thể mã nguồn
Chương trình dùng Queue<Customer> để lưu và xử lý khách hàng theo thứ tự đến. Mỗi khách có số thứ tự được đánh dấu bằng mã ID, thời điểm đến và thời gian chờ phục vụ. sau khi đọc dữ liệu, chương trình đưa lần lượt các khách vào cuối hàng đợi của queue.
Ban đầu, mọi quầy đều rảnh tại thời điểm 0. Mảng free lưu thời điểm rảnh tiếp theo của từng quầy, dùng vòng lặp while để lấy từng khách hàng ở đầu mảng bằng Dequeue(), sau đó quát mảng free để chọn quầy rảnh sớm nhất. nếu nhiều quầy có cùng thời điểm rảnh, quầy có số thứ tự nhỏ nhất sẽ được chọn.
Với mỗi khách hàng, thời điểm bắt đầu phục vụ là tính bằng thời điểm khách đến và thời điểm quầy rảnh. Thời điểm hoàn thành bằng điểm bắt đầu cộng thời gian phục vụ. Sau đó, chương trình cập nhật thời điểm của quầy, cộng dồn thời gian chờ và cập nhật thời gian chờ lớn nhất.
In thông tin của từng khách hàng. Khi hàng đợi rỗng, chương trình tính thời gian chờ trung bình và in ra kết quả. Thời điểm hoàn thành tất cả khách là thời điểm kết thúc lớn nhất, vì khách được xét sau cùng có thể kết thúc trước một khách đang được phục vụ tại quầy khác.
Độ phức tạp của thời gian là Θ(nk) có n khách, mỗi khách cần quét k quầy. Bộ nhớ là Θ(n) cho hàng đợi và Θ(k)  cho mảng thời điểm rảnh.

Bài 3: 
Mã code:
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
Kết quả: 
 
 
Giải thích:
9.	Front: là chỉ số phần tử đầu hàng, được đọc bởi Peek() hoặc lấy ra bằng Dequeue(); rear: là chỉ số ô sẽ ghi phần tử mới khi Enqueue(); count: là số phần tử hiện có; Capacity: là số ô của mảng
10.	Vì sao front == rear có thể là rỗng hoặc đầy: khi hàng rỗng, không có phần tử nào giữa đầu hàng và vị trí thêm nên front == rear. Khi hàng đầy, rear đã đi hết một vòng và quay lại bằng front. Nên chỉ so sánh hai chỉ số không thể phân biệt hai trạng thái.
11.	Công thức (i + 1) % Capacity hoạt động thế nào ở cuối mảng. Công thức tăng chỉ số lên một và đưa chỉ số về 0 khi đi qua ô cuối mảng. nhờ đó, front và rear có thể quay lại đầu mảng để sử dụng các ô trống, không vượt giới hạn mảng và không cần dồn phần tử.
12.	Vì sao mở rộng phải chép từ front và đặt lại  front = 0, vì: trong hàng đợi vòng, thứ tự lưu trong mảng có thể khác thứ tự vào trước ra trước. khi mở rộng mảng thì mảng mới sẽ chứa các phần từ bắt đầu từ vị trí mở rộng mảng. sau khi chép, đặt front = 0 và rear = count để chỉ đúng đầu hàng và ô thêm tiếp theo. Nếu chép nguyên mảng cũ rồi đặt front = 0 thì thứ tự lấy ra không đúng theo quy tắt FIFO.
13.	Vì sao Enqueue() là O(1) khấu hao, còn Dequeue() luôn O(1), vì: Enqueue() chỉ ghi một phần tử, cập nhật rear tăng count, nên tốn O(1). Khi mảng đầy, thao tác phai chép n phần tử sang mảng mới nên lần đó tốn O(n). nhưng nếu dung lượng tăng gấp đôi thì tổng số phần tử được chép qua là cấp số nhân, nên chi phí trung bình mỗi lần thêm là O(1) khấu hao. Còn Dequeue() chỉ đọc và xóa một, dịch front giảm count, không có vòng lặp hoặc dịch chuyển dữ liệu nên luôn là O(1).
Giải thích tổng thể mã code:
Chương trình cài đặt lớp CircularQueue<T> bằng một mảng có dung lượng ban đầu là 4. Kiểu tổng quát T cho phép hàng đợi lưu nhiều loại dữ liệu như số nguyên hoặc ký tự. Ba biến front, rear, count lần lượt quản lý đầu hàng, vị trí thêm tiếp theo và số phần tử hiện có.
Khi thêm phần tử bằng Enqueue(), chương trình kiểm tra mảng đã đầy chưa. Nếu chưa đầy, phần tử được ghi vào ô rear, sau đó rear chuyển sang ô tiếp theo bằng phép chia dư và count tăng một. Nếu mảng đầy, hàm Grow() tạo mảng lớn gấp đôi và chép các phần tử theo thứ tự FIFO, bắt đầu từ front. Sau đó, chương trình cập nhật mảng mới, đặt front = 0, rear = count rồi tiếp tục thêm.
Khi lấy phần tử bằng Dequeue(), chương trình kiểm tra hàng rỗng. Nếu rỗng, chương trình ném InvalidOperationException. Nếu có phần tử, chương trình lưu giá trị tại front, đặt ô đó về giá trị mặc định, dịch front sang ô tiếp theo và giảm count. Các phần tử còn lại giữ nguyên vị trí nên thao tác không cần dồn mảng.
Hàm Peek() cũng kiểm tra hàng rỗng nhưng chỉ trả về phần tử đầu hàng, không xóa hoặc thay đổi trạng thái. Hai thuộc tính Count và Capacity cho phép đọc số phần tử và dung lượng hiện tại.
Trong Main(), chương trình chạy lần lượt bảy ca kiểm thử để kiểm tra hàng rỗng, hàng đầy, quay vòng chỉ số và mở rộng mảng. Hai ca lớn kiểm tra giá trị lấy ra có đúng thứ tự hay không; ca cuối dùng Stopwatch đo thời gian thêm và lấy hết 100.000 phần tử.
bài 1:
1	{(a+b)x[c-d]}	HOP LE	HOP LE	Có	Ngoặc đúng cặp và thứ tự
2	{(a+b]xc)	SAI tai vi tri 5	SAI tai vi tri 5	Có	Ngoặc đóng sai loại
3	(a+b	SAI tai vi tri 0	SAI tai vi tri 0	Có	Ngoặc mở chưa đóng
4	a+b)	SAI tai vi tri 3	SAI tai vi tri 3	Có	Ngoặc đóng thừa
5	Không nhập gì, chỉ nhấn Enter	HOP LE	HOP LE	Có	Xử lý chuỗi có độ dài 0
6	([)]	SAI tai vi tri 2	SAI tai vi tri 2	Có	Ngoặc đóng sai thứ tự
7	a+b	HOP LE	HOP LE	Có	Bỏ qua ký tự không phải ngoặc
8	{[(	SAI tai vi tri 0	SAI tai vi tri 0	Có	Báo ngoặc mở chưa đóng sớm nhất
9	500.000 dấu ( rồi 500.000 dấu )	HOP LE; thời gian dưới 1.000 ms	HOP LE, 0.004ms 	Có	Chuỗi lớn, ngoặc cân bằng
10	1.000.000 dấu (	SAI tai vi tri 0; thời gian dưới 1.000 ms	SAI tai vi tri 0; 33.619ms	Có	Stack sâu nhất, toàn ngoặc mở
bài 2: 
1	n = 3, k = 1; (0,5), (1,3), (2,4)	Quầy: 1, 1, 1; bắt đầu: 0, 5, 8; chờ: 0, 4, 6; xong: 5, 8, 12; TB: 3.33; chờ lâu nhất: 6; hoàn thành: 12	KH 1: đến 0, quầy 1, bắt đầu 0, chờ 0, xong 5
KH 2: đến 1, quầy 1, bắt đầu 5, chờ 4, xong 8
KH 3: đến 2, quầy 1, bắt đầu 8, chờ 6, xong 12
Thời gian chờ trung bình: 3.33
Chờ lâu nhất: 6
Hoàn thành lúc: 12	Có	Một quầy, khách phải chờ
2	n = 3, k = 2; (0,5), (1,3), (2,4)	Quầy: 1, 2, 2; bắt đầu: 0, 1, 4; chờ: 0, 0, 2; xong: 5, 4, 8; TB: 0.67; chờ lâu nhất: 2; hoàn thành: 8	KH 1: đến 0, quầy 1, bắt đầu 0, chờ 0, xong 5
KH 2: đến 1, quầy 2, bắt đầu 1, chờ 0, xong 4
KH 3: đến 2, quầy 2, bắt đầu 4, chờ 2, xong 8
Thời gian chờ trung bình: 0.67
Chờ lâu nhất: 2
Hoàn thành lúc: 8	Có	Chọn quầy rảnh sớm nhất
3	n = 3, k = 2; (0,3), (0,3), (0,3)	Quầy: 1, 2, 1; bắt đầu: 0, 0, 3; chờ: 0, 0, 3; xong: 3, 3, 6; TB: 1.00; chờ lâu nhất: 3; hoàn thành: 6	KH 1: đến 0, quầy 1, bắt đầu 0, chờ 0, xong 3
KH 2: đến 0, quầy 2, bắt đầu 0, chờ 0, xong 3
KH 3: đến 0, quầy 1, bắt đầu 3, chờ 3, xong 6
Thời gian chờ trung bình: 1.00
Chờ lâu nhất: 3
Hoàn thành lúc: 6	Có	Cùng thời điểm rảnh, chọn quầy số nhỏ
4	n = 2, k = 1; (0,2), (10,3)	Quầy: 1, 1; bắt đầu: 0, 10; chờ: 0, 0; xong: 2, 13; TB: 0.00; chờ lâu nhất: 0; hoàn thành: 13	KH 1: đến 0, quầy 1, bắt đầu 0, chờ 0, xong 2
KH 2: đến 10, quầy 1, bắt đầu 10, chờ 0, xong 13
Thời gian chờ trung bình: 0.00
Chờ lâu nhất: 0
Hoàn thành lúc: 13	Có	Quầy rảnh trước khi khách đến
5	n = 1, k = 3; (4,6)	Quầy: 1; bắt đầu: 4; chờ: 0; xong: 10; TB: 0.00; chờ lâu nhất: 0; hoàn thành: 10	KH 1: đến 4, quầy 1, bắt đầu 4, chờ 0, xong 10
Thời gian chờ trung bình: 0.00
Chờ lâu nhất: 0
Hoàn thành lúc: 10	Có	Số quầy lớn hơn số khách
6	n = 100.000, k = 10; khách thứ i đến lúc i − 1, phục vụ trong 10 đơn vị	Mọi khách chờ 0; TB: 0.00; chờ lâu nhất: 0; hoàn thành: 100009; thời gian mô phỏng mong đợi dưới 1.000 ms	Cho trung binh: 0.00
Cho lau nhat: 0
Hoan thanh luc: 100009
Thoi gian mo phong: 3.525 ms	Có	Ca lớn; ghi rõ phạm vi đo thời gian
bài 3: 
1	Tạo hàng mới; gọi Dequeue(), Peek()	Count: 0; Capacity: 4; cả hai thao tác đều ném InvalidOperationException	Count: 0, Capacity: 4
Dequeue: InvalidOperationException
Peek: InvalidOperationException	Có	Xử lý hàng rỗng
2	Enqueue A, B, C, D; Peek	Peek: A; Count sau Peek: 4; Capacity: 4	Peek: A
Count: 4, Capacity: 4	Có	Hàng vừa đầy; Peek không xóa
3	Enqueue A, B, C, D; Dequeue 2 lần; Enqueue E, F; Dequeue 4 lần	Lấy trước: A, B; lấy tiếp: C, D, E, F; Count cuối: 0; Capacity: 4	Lay ra truoc: A, B
Lay ra tiep: C D E F
Count: 0, Capacity: 4	Có	Quay vòng chỉ số, chưa mở rộng
4	Enqueue 1, 2, 3, 4, 5; Dequeue 5 lần	Lấy ra: 1, 2, 3, 4, 5; Count cuối: 0; Capacity: 8	Lay ra: 1 2 3 4 5
Count: 0, Capacity: 8	Có	Mở rộng khi front = 0
5	Enqueue A, B, C, D; Dequeue 2 lần; Enqueue E, F, G; Dequeue 5 lần	Lấy trước: A, B; lấy tiếp: C, D, E, F, G; Count cuối: 0; Capacity: 8	Lay ra truoc: A, B
Lay ra tiep: C D E F G
Count: 0, Capacity: 8	Có	Mở rộng khi front ≠ 0, giữ FIFO
6	Lặp 1.000.000 lần: Enqueue i rồi Dequeue	Giá trị lấy ra đúng bằng i; số lần sai: 0; Count cuối: 0; Capacity: 4	So lan sai: 0
Count: 0, Capacity: 4	Có	Quay vòng nhiều lần
7	Enqueue 100.000 số từ 0 đến 99.999 rồi Dequeue hết	Lấy đúng thứ tự 0 đến 99.999; số phần tử sai: 0; Count cuối: 0; Capacity: 131072; ghi thời gian chạy	So phan tu sai thu tu: 0
Count: 0, Capacity: 131072
Thoi gian: 3.168 ms	Có	Ca lớn, mở rộng nhiều lần
