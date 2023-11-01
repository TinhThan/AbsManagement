import { decode, encode } from 'js-base64';

export function JsonToBase64(data: object): string {
    const objJsonStr = JSON.stringify(data);

    if (process.env.NODE_ENV === 'development') {
        return objJsonStr;
    }
    const objJsonB64 = encode(objJsonStr);
    return objJsonB64;
}

export function Base64ToJson<T>(base64: string): T {
    if (process.env.NODE_ENV === 'development') {
        return JSON.parse(base64);
    }

    return JSON.parse(decode(base64));
}
