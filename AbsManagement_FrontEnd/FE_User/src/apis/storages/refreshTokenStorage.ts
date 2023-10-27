import { StorageKey } from "../../constants/storageKey";
import LocalStore from "../../utils/store";

interface IRefreshTokenStorage{
    get(): string;
    set(data:string):void;
    remove(): void;
}

const RefreshTokenStorage: IRefreshTokenStorage ={
    get(): string{
        return LocalStore.getValue(StorageKey.REFRESHTOKEN) || '';
    },
    set(data: string){
        return localStorage.setItem(StorageKey.REFRESHTOKEN,data);
    },
    remove(){
        localStorage.removeItem(StorageKey.REFRESHTOKEN);
    }
};

export default RefreshTokenStorage;