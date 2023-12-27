﻿namespace AbsManagementAPI.Core.Constants
{
    public class MessageSystem
    {
        public const string DATA_INVALID = "Dữ liệu không hợp lệ.";
        public const string SERVER_ERROR = "Đã xảy ra lỗi ở hệ thống.";
        public const string TOKEN_EXPIRED = "Token đã hết hạn.";
        public const string AUTH_AUTHENTICATED_ERROR = "Đăng nhập thất bại.";
        public const string AUTH_INVALID = "Thông tin tài khoản không hợp lệ.";
        public const string REFRESH_TOKEN_INVALID = "Refresh token không hợp lệ.";
        public const string REFRESH_TOKEN_EXPIRED = "Phiên làm việc của bạn đã hết hạn.";

        public const string ADD_FAIL = "Thêm mới thất bại.";
        public const string ADD_SUCCESS = "Thêm mới thành công.";

        public const string UPDATE_SUCCESS = "Cập nhật thành công.";
        public const string UPDATE_FAIL = "Cập nhật thất bại.";

        public const string DELETE_SUCCESS = "Xóa thành công.";
        public const string DELETE_FAIL = "Xóa thất bại.";

        public const string APPROVE_SUCCESS = "Duyệt thành công.";
        public const string APPROVE_FAIL = "Duyệt thất bại.";


        public const string VERSION_UPDATE = "Dữ liệu đã được thay đổi bởi người sửa: <b>{1}</b>, lúc: <b>{2}</b><br/>Vui lòng làm mới lại trang";
    }
}
