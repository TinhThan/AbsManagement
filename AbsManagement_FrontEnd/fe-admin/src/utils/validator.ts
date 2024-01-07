export const validateDatePicker:any = (rule, value) => {
    const today = new Date();
    const diff = today.getFullYear() - value.getFullYear();
    if (diff < 18) {
      return "Độ tuổi quy định là trên 18 tuổi!";
    }
    return true;
};

export const messageValidate = {
  RequireEmail: "Vui lòng nhập email.",
  RequireEmailInvalid: "Email không hợp lệ.",
  RequireName: "Vui lòng nhập họ tên.",
  RequireDiaChi: "Vui lòng nhập địa chỉ.",
  RequireSoDienThoai: "Vui lòng nhập số điện thoại.",
  RequireTenCongTy: "Vui lòng nhập tên công ty.",
  RequireLoaiBangQuangCao: "Vui lòng chọn loại bảng quảng cáo.",
  RequireLoaiViTri: "Vui lòng chọn loại vị trí.",
  RequireHinhThucQuangCao: "Vui lòng chọn hình thức quảng cáo.",
  RequireHinhThucBaoCao: "Vui lòng chọn hình thức báo cáo.",
  RequireNgayHetHan: "Vui lòng chọn ngày hết hạn.",
  RequireNgayBatDau: "Vui lòng chọn ngày bắt đầu.",
  RequireNgaySinh: "Vui lòng chọn ngày sinh.",
  RequireNgaySinhCanBo: "Ngày sinh cán bộ phải trên 18 tuổi.",
  RequirePhuong: "Vui lòng chọn phường.",
  RequireQuan: "Vui lòng chọn quận.",
  RequireQuyen: "Vui lòng chọn quyền.",
  RequireKichThuoc: "Vui lòng nhập kích thước.",
  RequireTinhTrang: "Vui lòng nhập tình trạng."
}