import { Base64ToJson, JsonToBase64 } from '../../utils/base64';
import { UserStorage } from '../models/user';
import { StorageKey } from '../../constants/storageKey';
import LocalStore from '../../utils/store';

interface IUserInfoStorage {
  get(): UserStorage;
  set(data: UserStorage): void;
  remove(): void;
}
const UserInfoStorage: IUserInfoStorage = {
  get(): UserStorage {
    const _localUser = LocalStore.getValue(StorageKey.USER);
    if (_localUser && typeof _localUser === 'string') {
      return Base64ToJson<UserStorage>(_localUser);
    }
    return {
      taiKhoan: '',
      tenNhanVien: ''
    };
  },
  set(data: UserStorage): void {
    LocalStore.setValue(StorageKey.USER, JsonToBase64(data));
  },
  remove(): void {
    LocalStore.removeValue(StorageKey.USER);
  },
};
export default UserInfoStorage;
