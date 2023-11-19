function stringify<T>(value: T):string{
    return JSON.stringify(value);
}

function parse<T>(value: string):T| null{
    try{
        return JSON.parse(value) as T;
    }catch(error){
        return null;
    }
}

interface ILocalStore{
    setValue<T>(key:string,data: T): ILocalStore;
    getValue(key:string): string;
    removeValue(key:string): ILocalStore;
}

const LocalStore : ILocalStore = {
    setValue<T>(key: string, data: T): ILocalStore{
        LocalStore.setValue(key,stringify(data));
        return this;
    },
    getValue(key:string): string{
        const value = localStorage.getItem(key);
        return value || "";
    },
    removeValue(key:string): ILocalStore{
        LocalStore.removeValue(key);
        return this;
    }
}

export default LocalStore;