import moment, {Moment } from 'moment-timezone'

export function GetDateTimeByFormat(value?: Date | null, format?: string) : string{
    return value ? moment(value).format(format) : '';
}

export function GetDateTime(value?:Date): Moment{
    return moment(value);
}

export const FormatTime = {
    DDMMYYYY_HHMMA: 'DD/MM/yyyy hh:mm A',
    DDMMYYYY_HHMMSSA: 'DD/MM/yyyy hh:mm:ss',
    YYYYMMDD_HHMMSSA: 'yyyy/MM/DD hh:mm:ss',
    DDMMYYYY: 'DD/MM/yyyy',
    YYYYMMDD: 'YYYY-MM-DD',
    DD_MM_YYYY: 'DD-MM-yyyy',
    YYYYMMDDTHHmmss: 'YYYY-MM-DDTHH:mm:ss',
    HH_MM_SS: 'HH:mm:ss',
    yyyy_MM_DD_HHmmss: 'YYYY-MM-DD hh:mm:ss',
    YYYY_MM_DDTHHmmss: 'YYYY-MM-DDTHH:mm:ss',
    ParseDateToTimeZone0: 'yyyy-MM-DDT00:00:00Z',
    ParseDateToTimeZoneFromDate: 'yyyy-MM-DDTHH:mm:ss.SSSZ',
    ParseDateToTimeZoneToDate: 'yyyy-MM-DDTHH:mm:ss.SSSZ',
    YYYYMMDDHHmmss: 'YYYYMMDDHHmmss',
    MMYYYY: 'MM/YYYY'
}