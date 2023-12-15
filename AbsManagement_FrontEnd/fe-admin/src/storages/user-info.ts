import { Base64ToJson } from '../utils/base64';
import { UserStorage } from '../apis/auth/user';
import { StorageKey } from '../constants/storageKey';
import LocalStore from '../utils/store';

interface IUserInfoStorage {
  get(): UserStorage | null;
  set(data: UserStorage): void;
  remove(): void;
}
const UserInfoStorage: IUserInfoStorage = {
  get(): UserStorage | null {
    const _localUser = LocalStore.getValue(StorageKey.USER);
    if (_localUser) {
      return Base64ToJson<UserStorage>(_localUser);
    }
    return null;
  },
  set(data: UserStorage): void {
    LocalStore.setValue(StorageKey.USER, JSON.stringify(data));
  },
  remove(): void {
    LocalStore.removeValue(StorageKey.USER);
  },
};
export default UserInfoStorage;
