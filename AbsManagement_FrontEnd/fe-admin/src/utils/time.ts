import dayjs from "dayjs";

export function GetDateTimeByFormat(value?: Date | null, format?: string) : string{
    return value ? dayjs(value).format(format) : '';
}

export function GetDateTime(value?:Date){
    return dayjs(value);
}

export const FormatTime = {
    DDMMYYYY_HHMMA: 'DD/MM/YYYY hh:mm A',
    DDMMYYYY_HHMMSSA: 'DD/MM/YYYY hh:mm:ss',
    YYYYMMDD_HHMMSSA: 'YYYY/MM/DD hh:mm:ss',
    DDMMYYYY: 'DD/MM/YYYY',
    YYYYMMDD: 'YYYY-MM-DD',
    DD_MM_YYYY: 'DD-MM-YYYY',
    YYYYMMDDTHHmmss: 'YYYY-MM-DDTHH:mm:ss',
    HH_MM_SS: 'HH:mm:ss',
    yyyy_MM_DD_HHmmss: 'YYYY-MM-DD hh:mm:ss',
    YYYY_MM_DDTHHmmss: 'YYYY-MM-DDTHH:mm:ss',
    ParseDateToTimeZone0: 'YYYY-MM-DDT00:00:00Z',
    ParseDateToTimeZoneFromDate: 'YYYY-MM-DDTHH:mm:ss.SSSZ',
    ParseDateToTimeZoneToDate: 'YYYY-MM-DDTHH:mm:ss.SSSZ',
    YYYYMMDDHHmmss: 'YYYYMMDDHHmmss',
    MMYYYY: 'MM/YYYY'
}