using System;
using System.Collections.Generic;
using System.Text;

namespace CTDLGT_Buoi03_33254020040.Bai1
{
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

        static void Main555()
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
}
