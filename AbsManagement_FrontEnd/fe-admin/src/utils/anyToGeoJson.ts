import { BaoCaoViPhamModel } from "../apis/baoCaoViPham/baoCaoViPhamModel"
import { DiemDatQuangCaoModel } from "../apis/diemDatQuangCao/diemDatQuangCaoModel"

export function SpaceToGeoJson(spaces: DiemDatQuangCaoModel[], daQuyHoach: boolean): any {
    const geoJSONFeatures = spaces.filter(space=> daQuyHoach ? space.idTinhTrang === 'DaQuyHoach' : space.idTinhTrang !== 'DaQuyHoach' ).map(space => {
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


export function ReportToGeoJson(spaces:DiemDatQuangCaoModel[], reports:BaoCaoViPhamModel[]) {
    const geoJSONFeatures = reports.map(report => {
        const space = spaces.find(t=>t.id === report.idDiemDatQuangCao);
        let coordinates = report.danhSachViTri;
        console.log("coordinates",coordinates)
        if(space)
        {
            coordinates = [report.danhSachViTri[0] -  0.00002696274,report.danhSachViTri[1]]
        }
        return {
            type: 'Feature',
            geometry: {
                type: 'Point',
                coordinates: coordinates // Là một mảng chứa [longitude, latitude]
            },
            properties: {
                id: report.id,
                idTinhTrang: report.idTinhTrang
            }
        }
    })
    return {
        type: 'FeatureCollection',
        features: geoJSONFeatures
    }
}