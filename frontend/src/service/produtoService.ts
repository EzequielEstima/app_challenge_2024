import axios from "axios";
import { Produto } from "../dataModels/Produto";
import { devEnvironment } from "../environments/devEnvironments";
import { ListaProdutos } from "../dataModels/ListaProdutos";


const PRODUTOS_URL = devEnvironment.BACKEND_URL + "/Products";

export class ProdutoService{
    async getProdutos(): Promise<ListaProdutos>{
      let res = await axios.request<ListaProdutos>({
        method: 'GET',
        url: PRODUTOS_URL
      })
      
      return res.data;
    }

    async getProdutoById(id: number){
      
      let res = await axios.request<Produto>({
        method: 'GET',
        url: PRODUTOS_URL + `/${id}`
      })

      return res.data;
  
      //TODO handle error
    }
}