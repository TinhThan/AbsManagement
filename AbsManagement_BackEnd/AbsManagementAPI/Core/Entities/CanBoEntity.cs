﻿using AbsManagementAPI.Entities;

namespace AbsManagementAPI.Core.Entities
{
    public class CanBoEntity
    {
        public CanBoEntity()
        {
            BaoCaoViPhams = new HashSet<BaoCaoViPhamEntity>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string HoTen { get; set; }
        public string SoDienThoai { get; set; }
        public DateTimeOffset NgaySinh { get; set; }
        public string Role { get; set; }
        public string MatKhau { get; set; }
        public string? RefreshToken { get; set; }
        public DateTimeOffset? RefreshTokenExpiryTime { get; set; }
        public string NoiCongTac { get; set; }
        public DateTime? NgayCapNhat { get; set; }
        public int? EmailVerified { get; set; }
        public string? PasswordResetOTP { get; set; }
        public DateTime? PasswordResetOTPExpiration { get; set; }

        public virtual ICollection<BaoCaoViPhamEntity> BaoCaoViPhams { get; set; }
        public virtual ICollection<PhieuCapPhepSuaQuangCaoEntity> PhieuCapPhepSuaQuangCaoGuis { get; set; }
        public virtual ICollection<PhieuCapPhepSuaQuangCaoEntity> PhieuCapPhepSuaQuangCaoDuyets { get; set; }
        public virtual ICollection<PhieuCapPhepQuangCaoEntity> PhieuCapPhepQuangCaoDuyets { get; set; }
        public virtual ICollection<PhieuCapPhepQuangCaoEntity> PhieuCapPhepQuangCaoGuis { get; set; }
    }
}
