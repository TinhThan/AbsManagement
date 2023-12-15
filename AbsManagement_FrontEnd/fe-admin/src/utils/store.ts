interface ILocalStore{
    setValue(key:string,data: string): ILocalStore;
    getValue(key:string): string;
    removeValue(key:string): ILocalStore;
}

const LocalStore : ILocalStore = {
    setValue(key: string, data: string): ILocalStore{
        localStorage.setItem(key,data);
        return this;
    },
    getValue(key:string): string{
        const value = localStorage.getItem(key);
        return value || "";
    },
    removeValue(key:string): ILocalStore{
        localStorage.removeItem(key);
        return this;
    }
}

export default LocalStore;