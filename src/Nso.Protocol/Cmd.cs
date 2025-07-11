namespace Nso.Protocol;

public enum Cmd : sbyte
{

    LOGIN = -127,
    REGISTER = -126,
    CLIENT_INFO = -125,
    SEND_SMS = -124,
    REGISTER_IMEI = -123,
    LOGIN0 = 0,
    REGISTER0 = 1,
    CLEAR_RMS = 2,
    FORGET_PASS = -122,
    FORGET_PASS_IMEI = -121,
    CHECK_KEY1 = -76,
    CHECK_KEY2 = -75,
    CHECK_KEY3 = -74,
    CHECK_KEY4 = -73,
    LAT_HINH = -72,
    CLEAR_TRANSACTION_ID = -71,
    MOI_GTC = -70,
    MOI_TATCA_GTC = -69,
    GO_GTCHIEN = -68,
    LOGOUT = -127,
    SELECT_PLAYER = -126,
    CREATE_PLAYER = -125,
    DELETE_PLAYER = -124,
    UPDATE_VERSION = -123,
    UPDATE_DATA = -122,
    UPDATE_MAP = -121,
    UPDATE_SKILL = -120,
    UPDATE_ITEM = -119,
    UPDATE_VERSION_OK = -118,
    UPDATE_PK = -117,
    UPDATE_OUT_CLAN = -116,
    REQUEST_ICON = -115,
    REQUEST_CLAN_LOG = -114,
    REQUEST_CLAN_INFO = -113,
    REQUEST_CLAN_MEMBER = -112,
    REQUEST_CLAN_ITEM = -111,
    REQUEST_SKILL = -110,
    REQUEST_MAPTEMPLATE = -109,
    REQUEST_NPCTEMPLATE = -108,
    REQUEST_NPCPLAYER = -107,
    ME_LOAD_ACTIVE = -106,
    ME_ACTIVE = -105,
    ME_UPDATE_ACTIVE = -104,
    ME_OPEN_LOCK = -103,
    ME_CLEAR_LOCK = -102,
    CLIENT_OK = -101,
    CLIENT_OK_INMAP = -100,
    INPUT_CARD = -99,
    CLEAR_TASK = -98,
    CHANGE_NAME = -97,
    CREATE_CLAN = -96,
    CLAN_CHANGE_ALERT = -95,
    CLAN_CHANGE_TYPE = -94,
    CLAN_MOVEOUT_MEM = -93,
    CLAN_OUT = -92,
    CLAN_UP_LEVEL = -91,
    INPUT_COIN_CLAN = -90,
    OUTPUT_COIN_CLAN = -89,
    CONVERT_UPGRADE = -88,
    INVITE_CLANDUN = -87,
    NOT_USEACC = -86,
    ITEM_SPLIT = -85,
    POINT_PB = -84,
    REVIEW_PB = -83,
    REWARD_PB = -82,
    CHIENTRUONG_INFO = -81,
    REVIEW_CT = -80,
    REWARD_CT = -79,
    CHAT_ADMIN = -78,
    CHANGE_BG_ID = -77,
    OAN_HON = -67,
    OAN_HON1 = -66,
    NAP_NOKIA = -65,
    GET_PASS2 = -64,
    NAP_GOOGLE = -63,
    OPEN_CLAN_ITEM = -62,
    CLAN_SEND_ITEM = -61,
    CLAN_USE_ITEM = -60,
    GPS = -59,
    ME_LOAD_ALL = -127,
    ME_LOAD_CLASS = -126,
    ME_LOAD_SKILL = -125,
    ME_LOAD_LEVEL = -124,
    ME_LOAD_INFO = -123,
    ME_LOAD_HP = -122,
    ME_LOAD_MP = -121,
    PLAYER_LOAD_ALL = -120,
    PLAYER_LOAD_INFO = -119,
    PLAYER_LOAD_LEVEL = sbyte.MinValue,
    PLAYER_LOAD_VUKHI = -117,
    PLAYER_LOAD_AO = -116,
    PLAYER_LOAD_QUAN = -113,
    PLAYER_LOAD_BODY = -112,
    PLAYER_LOAD_HP = -111,
    PLAYER_LOAD_LIVE = -110,
    POTENTIAL_UP = -109,
    SKILL_UP = -108,
    BAG_SORT = -107,
    BOX_SORT = -106,
    BOX_COIN_IN = -105,
    BOX_COIN_OUT = -104,
    REQUEST_ITEM = -103,
    USE_BOOK_SKILL = -102,
    ME_ADD_EFFECT = -101,
    ME_EDIT_EFFECT = -100,
    ME_REMOVE_EFFECT = -99,
    PLAYER_ADD_EFFECT = -98,
    PLAYER_EDIT_EFFECT = -97,
    PLAYER_REMOVE_EFFECT = -96,
    MAP_TIME = -95,
    NPC_PLAYER_UPDATE = -94,
    CHANGE_TYPE_PK = -93,
    UPDATE_TYPE_PK = -92,
    UPDATE_BAG_COUNT = -91,
    TASK_FOLLOW_FAIL = -90,
    END_WAIT = -89,
    CREATE_PARTY = -88,
    CHANGE_TEAMLEADER = -87,
    MOVE_MEMBER = -86,
    REQUEST_FRIEND = -85,
    REQUEST_ENEMIES = -84,
    FRIEND_REMOVE = -83,
    ENEMIES_REMOVE = -82,
    ME_UPDATE_PK = -81,
    ITEM_BODY_CLEAR = -80,
    BUFF_LIVE = -79,
    CALL_EFFECT_ME = -78,
    FIND_PARTY = -77,
    LOCK_PARTY = -76,
    ITEM_BOX_CLEAR = -75,
    SHOW_WAIT = -74,
    CALL_EFFECT_NPC = -73,
    ME_LOAD_GOLD = -72,
    ME_UP_GOLD = -71,
    ADMIN_MOVE = -70,
    ME_LOAD_THU_NUOI = -69,
    PLAYER_LOAD_THU_NUOI = -68,
    SAVE_RMS = -67,
    LOAD_RMS = -65,
    PLAYER_LOAD_MAT_NA = -64,
    CLAN_INVITE = -63,
    CLAN_ACCEPT_INVITE = -62,
    CLAN_PLEASE = -61,
    CLAN_ACCEPT_PLEASE = -60,
    REFRESH_HP = -59,
    CALL_EFFECT_BALL = -58,
    CALL_EFFECT_BALL_1 = -57,
    PLAYER_LOAD_AO_CHOANG = -56,
    PLAYER_LOAD_GIA_TOC = -55,
    LOAD_THU_CUOI = -54,
    FULL_SIZE = -32,
    KEY_WINPHONE = -31,
    SUB_COMMAND = -30,
    NOT_LOGIN = -29,
    NOT_MAP = -28,
    GET_SESSION_ID = -27,
    SERVER_DIALOG = -26,
    SERVER_ALERT = -25,
    SERVER_MESSAGE = -24,
    CHAT_MAP = -23,
    CHAT_PRIVATE = -22,
    CHAT_SERVER = -21,
    CHAT_PARTY = -20,
    CHAT_CLAN = -19,
    MAP_INFO = -18,
    MAP_CHANGE = -17,
    MAP_CLEAR = -16,
    ITEMMAP_REMOVE = -15,
    ITEMMAP_MYPICK = -14,
    ITEMMAP_PLAYERPICK = -13,
    ME_THROW = -12,
    ME_DIE = -11,
    ME_LIVE = -10,
    ME_BACK = -9,
    ME_UP_COIN_LOCK = -8,
    ME_CHANGE_COIN = -7,
    PLAYER_THROW = -6,
    NPC_LIVE = -5,
    NPC_DIE = -4,
    NPC_ATTACK_ME = -3,
    NPC_ATTACK_PLAYER = -2,
    NPC_HP = -1,
    PLAYER_DIE = 0,
    PLAYER_MOVE = 1,
    PLAYER_REMOVE = 2,
    PLAYER_ADD = 3,
    PLAYER_ATTACK_N_P = 4,
    PLAYER_UP_EXP = 5,
    ITEMMAP_ADD = 6,
    ITEM_BAG_REFRESH = 7,
    ITEM_BAG_ADD = 8,
    ITEM_BAG_ADD_QUANTITY = 9,
    ITEM_BAG_CLEAR = 10,
    ITEM_USE = 11,
    ITEM_USE_CHANGEMAP = 12,
    ITEM_BUY = 13,
    ITEM_SALE = 14,
    ITEM_BODY_TO_BAG = 15,
    ITEM_BOX_TO_BAG = 16,
    ITEM_BAG_TO_BOX = 17,
    ITEM_USE_UPTOUP = 18,
    UPPEARL = 19,
    UPPEARL_LOCK = 20,
    UPGRADE = 21,
    SPLIT = 22,
    PLEASE_INPUT_PARTY = 23,
    ACCEPT_PLEASE_PARTY = 24,
    REQUEST_PLAYERS = 25,
    UPDATE_ACHIEVEMENT = 26,
    MOVE_FAST_NPC = 27,
    ZONE_CHANGE = 28,
    MENU = 29,
    OPEN_UI = 30,
    OPEN_UI_BOX = 31,
    OPEN_UI_PT = 32,
    OPEN_UI_SHOP = 33,
    OPEN_MENU_ID = 34,
    OPEN_UI_COLLECT = 35,
    OPEN_UI_ZONE = 36,
    OPEN_UI_TRADE = 37,
    OPEN_UI_SAY = 38,
    OPEN_UI_CONFIRM = 39,
    OPEN_UI_MENU = 40,
    SKILL_SELECT = 41,
    REQUEST_ITEM_INFO = 42,
    TRADE_INVITE = 43,
    TRADE_INVITE_ACCEPT = 44,
    TRADE_LOCK_ITEM = 45,
    TRADE_ACCEPT = 46,
    TASK_GET = 47,
    TASK_NEXT = 48,
    TASK_FINISH = 49,
    TASK_UPDATE = 50,
    NPC_MISS = 51,
    RESET_POINT = 52,
    ALERT_MESSAGE = 53,
    ALERT_OPEN_WEB = 54,
    ALERT_SEND_SMS = 55,
    TRADE_INVITE_CANCEL = 56,
    TRADE_CANCEL = 57,
    TRADE_OK = 58,
    FRIEND_INVITE = 59,
    PLAYER_ATTACK_NPC = 60,
    PLAYER_ATTACK_PLAYER = 61,
    HAVE_ATTACK_PLAYER = 62,
    OPEN_UI_NEWMENU = 63,
    MOVE_FAST = 64,
    TEST_INVITE = 65,
    TEST_INVITE_ACCEPT = 66,
    TEST_END = 67,
    ADD_CUU_SAT = 68,
    ME_CUU_SAT = 69,
    CLEAR_CUU_SAT = 70,
    PLAYER_UP_EXPDOWN = 71,
    ME_DIE_EXP_DOWN = 72,
    PLAYER_ATTACK_P_N = 73,
    USE_SKILL_MY_BUFF = 74,
    CREATE_BUNHIN = 75,
    NPC_ATTACK_BUNHIN = 76,
    CLEAR_BUNHIN = 77,
    NPC_CHANGE = 78,
    PARTY_INVITE = 79,
    PARTY_ACCEPT = 80,
    PARTY_CANCEL = 81,
    PLAYER_IN_PARTY = 82,
    PARTY_OUT = 83,
    FRIEND_ADD = 84,
    NPC_IS_DISABLE = 85,
    NPC_IS_MOVE = 86,
    ThuNuoi_ATTACK = 87,
    RETURN_POINT_MAP = 88,
    NPC_IS_FIRE = 89,
    NPC_IS_ICE = 90,
    NPC_IS_WIND = 91,
    OPEN_TEXT_BOX_ID = 92,
    VIEW_INFO = 93,
    REQUEST_ITEM_PLAYER = 94,
    ME_UP_COIN_BAG = 95,
    GET_TASK_ORDER = 96,
    GET_TASK_UPDATE = 97,
    CLEAR_TASK_ORDER = 98,
    TEST_DUN_INVITE = 99,
    TEST_DUN_LIST = 100,
    VIEW_INFO1 = 101,
    SEND_ITEM_TO_AUCTION = 102,
    LOAD_ITEM_AUCTION = 103,
    VIEW_ITEM_AUCTION = 104,
    BUY_ITEM_AUCTION = 105,
    TEST_GT_INVITE = 106,
    OPEN_UI_CONFIRM_ID = 107,
    ITEM_MON_TO_BAG = 108,
    OPEN_UI_MENU1 = 109,
    LUYEN_THACH = 110,
    TINH_LUYEN = 111,
    DOI_OPTION = 112,
    CAT_KEO = 113,
    NV_BIAN = 114,
    UPDATE_INFO_ME = 115,
    UPDATE_INFO_CHAR = 116,
    MAP_ITEM = 117,
    COMFIRM_ACCOUNT = 118,
    AUTO_ATTACK_MOVE = 119,
    DOI_MAT_KHAU = 120,
    RANKED_MATCH = 121,
    SERVER_ADD_MOB = 122,
    info_kiemduyet = 123,
    NGOCKHAM = 124,
    GET_EFFECT = 125,
    GIAODO = 126
}
