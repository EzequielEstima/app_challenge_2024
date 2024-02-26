import axios from "axios";
import { Produto } from "../dataModels/Produto";
import { devEnvironment } from "../environments/devEnvironments";


const PRODUTOS_URL = devEnvironment.BACKEND_URL + "/Products";

export class ProdutoService{
    async getProdutos(): Promise<Produto[]>{
      let res = await axios.request<Produto[]>({
        method: 'GET',
        url: PRODUTOS_URL
      })
      
      return res.data;

      //return produtos;
    }

    async getProdutoById(id: number){
      
      let res = await axios.request<Produto>({
        method: 'GET',
        url: PRODUTOS_URL + `/${id}`
      })

      return res.data;
  
      //TODO handle error


      // const res = produtos.find((prod) => prod.id === id)

      // if(!res) throw new Error("FML");

      // return res;
    }
}