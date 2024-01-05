import { BaoCaoViPhamModel } from "../apis/baoCaoViPham/baoCaoViPhamModel"
import { DiemDatQuangCaoModel } from "../apis/diemDatQuangCao/diemDatQuangCaoModel"

export function SpaceToGeoJson(spaces: DiemDatQuangCaoModel[], tinhTrang:string | null): any {
    const geoJSONFeatures = spaces.filter(space=> (tinhTrang !== null ?  space.idTinhTrang === tinhTrang : true) ).map(space => {
        return {
            type: 'Feature',
            geometry: {
                type: 'Point',
                coordinates: space.danhSachViTri
            },
            properties: {
                id:space.id
            },
            id: space.id
        }
    })
    return {
        type: 'FeatureCollection',
        features: geoJSONFeatures
    }
}

export function SpaceAnyToGeoJson(space: DiemDatQuangCaoModel | undefined): any {
    return {
        type: 'FeatureCollection',
        features: {
            type: 'Feature',
            geometry: {
                type: 'Point',
                coordinates: space?.danhSachViTri
            },
            properties: {
                id:space?.id
            },
            id: space?.id
        }
    }
}

export function ReportToGeoJson(reports: BaoCaoViPhamModel[]):any {
    const geoJSONFeatures = reports.map(report => {
        return {
            type: 'Feature',
            geometry: {
                type: 'Point',
                coordinates: report.danhSachViTri
            },
            properties: {
                id:report.id
            }
        }
    })
    return {
        type: 'FeatureCollection',
        features: geoJSONFeatures
    }
}