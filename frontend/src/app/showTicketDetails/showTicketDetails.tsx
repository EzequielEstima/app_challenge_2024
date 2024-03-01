import { Button, IconButton, Stack, Tooltip } from "@mui/material";
import { Link, useLocation, useNavigate, useParams } from "react-router-dom";
import { Ticket } from "../../dataModels/Ticket";
import "./showTicketDetails.css";
import { useEffect, useState } from "react";
import { TicketService } from "../../service/ticketService";
import { Produto } from "../../dataModels/Produto";
import { ProdutoService } from "../../service/produtoService";
import { abort } from "process";


const abortController = new AbortController();

export function ShowTicketDetails() {

  const navigate = useNavigate();

  const [ticket, setTicket] = useState<Ticket>();
  const [products, setProducts] = useState<Produto[]>([]);
  
  const {id} = useParams();
  
  useEffect(() => {
    if(!id) return; 

    const ticketService = new TicketService();
    ticketService.getTicketById(parseInt(id),abortController)
      .then((ticket) => setTicket(ticket))
      .catch((error) => {
        if (error.code === 'ERR_NETWORK') {
          abortController.abort(); // Abort other requests
          alert('Não foi possível ligar ao servidor');
          navigate('/')
        } else if (error.code !== "ERR_CANCELED"){ // Ignore aborted requests
          alert(`Não foi possível obter os detalhes do ticket \n\nERRO : ${error.response.data}`)
          navigate('/')
        }
      });

    const produtoService = new ProdutoService();
    produtoService.getProdutos(abortController)
    .then((listaProdutosModel) => setProducts(listaProdutosModel.products))
    .catch((error) =>{
      if (error.code === 'ERR_NETWORK') {
        abortController.abort(); // Abort other requests
        alert('Não foi possível ligar ao servidor');
        navigate('/')
      } else if(error.code !== "ERR_CANCELED"){ // Ignore aborted requests
        alert(`Não foi possível obter a lista de produtos \n\nERRO : ${error.response.data}`)
        navigate('/')
      }
    });
  }, [id]); // No dependencies, so it only runs once at the start



  return (
    <Stack className="stack-container" >
      <h2>Detalhes</h2>
      <p>Título: { ticket?.titulo ?? 'NA'}</p>  
      {/* Only null and undefiend */}
      <p>Descrição: { ticket?.descricao || 'NA'}</p>
      <p>Prioridade: {ticket ? ticket.prioridade : 'NA'}</p>
      <p>Produto: {ticket ? products?.find( (prod) => prod.productId === ticket.produtoId)?.nome : 'NA'}</p>
      
        {/* <Link to="/tickets">
          <Button variant="contained">Voltar</Button>
        </Link> 
        
        */}
      <Button variant="contained" onClick={() => navigate( `/tickets/${id}/edit`)}>Editar</Button>
      <Button variant="contained" onClick={() => navigate('/tickets')}>Voltar</Button>
      
    </Stack>
  );
}

