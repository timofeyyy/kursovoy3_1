//using System.Net;
//using System.Net.Mail;

//MailAddress from = new MailAddress("reciever@mailServ.com", "code");
//MailAddress to = new MailAddress("timofey12357@dfdfdfdf.com");

//MailMessage m = new MailMessage(from, to);
//m.Subject = "New user";
//m.Body = $"Пользователь имя зарегестрировался в проекте вот его емэйл @gmail.com!";


//SmtpClient smtp = new SmtpClient("192.168.100.15", 25)
//{
//    Credentials = new NetworkCredential("reciever@mailServ.com", "tp28032004")
//};

//smtp.Send(m);
//Console.Read();


//bool IsDomainValid(string email)
//{
//    try
//    {
//        var domain = email.Split('@')[1];
//        var hostEntry = Dns.GetHostEntry(domain); 
//        return hostEntry != null;
//    }
//    catch
//    {
//        return false;
//    }
//}

//Console.WriteLine(IsDomainValid("t.p.se@mailServ.com"));
using System.Drawing;
using System.Reflection;

List<Color> GetAllColors()
{
    List<Color> aallColors = new List<Color>();

    foreach (PropertyInfo property in typeof(Color).GetProperties())
    {
        if (property.PropertyType == typeof(Color))
        {
           Color color = (Color)property.GetValue(null);
           Console.WriteLine(color.Name.ToString());
        }
    }

    return aallColors;
}
Console.Clear();
Console.WriteLine("Введите цвет:");
List<Color>  allColors = GetAllColors();
for (int i = 1; i < allColors.Count(); i++)
{
    try
    {
        Console.WriteLine(allColors[i].Name + " ", allColors[i]);
    }
    catch { }
}
Console.ReadLine();