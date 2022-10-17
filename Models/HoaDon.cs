namespace _102190334_NguyenMinhQuang.Models;

public class HoaDon
{
    public int Id { get; set; }
    public string Ten { get; set; }
    public string MaHoaDon { get; set; }
    public string EmailKhachHang { get; set; }
    public string NgayTao { get; set; }
    public string HinhAnh { get; set; }
    public int LoaiHoaDonId { get; set; }
    public LoaiHoaDon LoaiHoaDon { get; set; }
}