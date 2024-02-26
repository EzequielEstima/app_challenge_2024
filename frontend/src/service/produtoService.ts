import axios from "axios";
import { Produto } from "../dataModels/Produto";

const produtos: Produto[] = [
    {
      id: 1,
      nome: "Produto 1"
    },
    {
      id: 2,
      nome: "Produto 2"
    },
    {
      id: 3,
      nome: "Produto 3"
    }
  ];
  

export class ProdutoService{
    async getProdutos(): Promise<Produto[]>{
    //   let res = await axios.request<Produto[]>({
    //     method: 'GET',
    //     headers:{
    //       'Accept': 'application/json',
    //     },
    //     url: 'https://run.mocky.io/v3/3a7dadf1-913e-496a-a33a-0be1451a6b94'
    //   })
    //     return res.data;

      return produtos;
    }

    async getProdutoById(id: number){
      const res = produtos.find((prod) => prod.id === id)

      if(!res) throw new Error("FML");

      return res;
    }
}