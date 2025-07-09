

namespace Nso.Protocol;

// Sub-command bên trong CMD -29
public enum  NotLoginSub : sbyte
{
    SET_CLIENT_TYPE = -125, // gói client gửi thông tin thiết bị
    LOGIN_REQUEST = -127, // user/pass
    // ... thêm nếu cần

}
