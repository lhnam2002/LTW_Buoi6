namespace BaiTapBuoi6
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhanVien")]
    public partial class NhanVien
    {
        [Key]
        [Column("Mã nhân viên")]
        [StringLength(6)]
        public string Mã_nhân_viên { get; set; }

        [Column("Tên nhân viên")]
        [Required]
        [StringLength(20)]
        public string Tên_nhân_viên { get; set; }

        [Column("Ngày sinh")]
        public DateTime Ngày_sinh { get; set; }

        [Column("Mã phòng ban")]
        [Required]
        [StringLength(2)]
        public string Mã_phòng_ban { get; set; }

        public virtual PhongBan PhongBan { get; set; }
    }
}
