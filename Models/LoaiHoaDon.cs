namespace _102190334_NguyenMinhQuang.Models;

public class LoaiHoaDon
{
    public int Id { get; set; }
    public string Ten { get; set; }
    public List<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
}