import data from '../assets/new-dataHCM.json'

export function getDistrictsAndWards(postcode:string) {
    const item = data[0].districts.find((obj) => obj.postcode === postcode);
    if(item)
        return item.ward;
    else
        return [];
}

export function getDistrictByCode(postcode:string) {
    const item = data[0].districts.find((obj) => obj.postcode === postcode);
    if(item)
        return item;
    else
        return {
            name:"5",
            postcode:"72700",
            center:[
                106.670375,
                10.756129
            ],
            id:"locality.91851508",
            ward:[]
        };
}

export function getDistrictWithCode(district : string) {
    const item = data[0].districts.find((obj) =>obj.postcode === district);
    if(item)
        return item;
    else
        return {
            name:"5",
            postcode:"72700",
            center:[
                106.670375,
                10.756129
            ],
            id:"locality.91851508",
            ward:[]
        };
}

export function getDistrictWithName(district : string) {
    const item = data[0].districts.find((obj) => district.includes(obj.name));
    if(item)
        return item;
    else
        return {
            name:"5",
            postcode:"72700",
            center:[
                106.670375,
                10.756129
            ],
            id:"locality.91851508",
            ward:[]
        };
}

export function getWardByDistrictWithName(district:string, ward:string) {
    const districtItem = data[0].districts.find((obj) =>obj.postcode === district);
    const item = districtItem?.ward.find((obj) =>ward.includes(obj.name));
    if(item)
        return item;
    else
        return {
            name: "9",
            postcode: "72712"
        };
}

export function getWardByDistrictWithCode(district:string, ward:string) {
    const districtItem = data[0].districts.find((obj) => obj.postcode === district);
    const item = districtItem?.ward.find((obj) => obj.postcode === ward);
    if(item)
        return item;
    else
        return {
            name: "9",
            postcode: "72712"
        };
}

export function getDistrictById(id:string) {
    const districtItem = data[0].districts.find((obj) => obj.id === id);
    if(districtItem)
        return districtItem;
    else
        return {
            name: "5",
            postcode: "72700",
            center:[
                106.670375,
                10.756129
            ],
            id:"locality.91851508",
            ward:[]
        };
}