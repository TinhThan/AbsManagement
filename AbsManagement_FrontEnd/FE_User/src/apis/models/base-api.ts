export interface ErrorInfo{
    errors?: {[key: string]: string[]};
    status: number;
    detail?: string;
    description?: string[];
}