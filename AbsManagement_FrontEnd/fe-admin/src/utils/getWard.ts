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
    console.log("item",item)
    if(item)
        return item;
    else
        return {
            name:"5",
            postcode:"72700"
        };
}

export function getDistrictByName(name:string) {
    const item = data[0].districts.find((obj) => name.includes(obj.name));
    if(item)
        return item;
    else
        return {
            name:"5",
            postcode:"72700"
        };
}

export function getWardByCode(postcodeDistrict:string,postCodeWard:string) {
    const district = data[0].districts.find((obj) => obj.postcode === postcodeDistrict);
    const item = district?.ward.find((obj) => obj.postcode === postCodeWard);
    if(item)
        return item;
    else
        return {
            name: "4",
            postcode: "72711"
        };
}

export function getWardByName(postcodeDistrict:string,name:string) {
    const district = data[0].districts.find((obj) => obj.postcode === postcodeDistrict);
    const item = district?.ward.find((obj) => name.includes(obj.name));
    if(item)
        return item;
    else
        return {
            name: "4",
            postcode: "72711"
        };
}