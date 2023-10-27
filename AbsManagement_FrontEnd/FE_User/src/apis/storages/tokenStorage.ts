import { StorageKey } from "../../constants/storageKey";
import LocalStore from "../../utils/store";

interface ITokenStorage{
    get(): string;
    set(data:string):void;
    remove(): void;
}

const TokenStorage: ITokenStorage ={
    get(): string{
        return LocalStore.getValue(StorageKey.TOKEN) || '';
    },
    set(data: string){
        return localStorage.setItem(StorageKey.TOKEN,data);
    },
    remove(){
        localStorage.removeItem(StorageKey.TOKEN);
    }
};

export default TokenStorage;