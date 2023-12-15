export function JsonToBase64(data: object): string {
    return JSON.stringify(data);
}

export function Base64ToJson<T>(base64: string): T {
    return JSON.parse(base64);
}
