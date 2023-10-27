import { Actor } from '../components/TableActor';
import { BaseApi } from './baseApi';
import { ConfigUrlApi } from './configs/configUrlApi';
import { API_URL } from '../constants/apiConfig';

class ActorApi extends BaseApi {
  async DanhSach(soLuong:string) {
    return this.get<Actor[]>(`${API_URL}${ConfigUrlApi.Urls.Actor.DanhSach}${soLuong}`);
  }

  async ChiTiet(id:number) {
    return this.get<Actor>(API_URL+ConfigUrlApi.Urls.Actor.ChiTiet+id);
  }
}

export interface IActorApi {
    DanhSach(soLuong:string): Promise<Actor[]>;
    ChiTiet(id:number): Promise<Actor>;
}

export const actorApi: IActorApi = new ActorApi();
