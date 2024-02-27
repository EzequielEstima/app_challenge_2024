import { Button, IconButton, Stack, Tooltip } from "@mui/material";
import { Link, useLocation, useNavigate, useParams } from "react-router-dom";
import { Ticket } from "../../dataModels/Ticket";
import "./showTicketDetails.css";
import { useEffect, useState } from "react";
import { TicketService } from "../../service/ticketService";
import { Produto } from "../../dataModels/Produto";
import { ProdutoService } from "../../service/produtoService";


export function ShowTicketDetails() {

  const navigate = useNavigate();

  const [ticket, setTicket] = useState<Ticket>();
  const [products, setProducts] = useState<Produto[]>([]);
  
  const {id} = useParams();
  
  useEffect(() => {
    if(!id) return; 

    const ticketService = new TicketService();
    ticketService.getTicketById(parseInt(id))
      .then((ticket) => setTicket(ticket))
      .catch(() => navigate('/tickets'));

    const produtoService = new ProdutoService();
    produtoService.getProdutos().then((listaProdutosModel) => setProducts(listaProdutosModel.products));

  }, [id]); // No dependencies, so it only runs once at the start



  return (
    <Stack className="stack-container">
      <h2>Detalhes</h2>
      <p>Título: { ticket?.titulo ?? 'NA'}</p>  
      {/* Only null and undefiend */}
      <p>Descrição: { ticket?.descricao || 'NA'}</p>
      <p>Prioridade: {ticket ? ticket.prioridade : 'NA'}</p>
      <p>Produto: {ticket ? products?.find( (prod) => prod.productId === ticket.produtoId)?.nome : 'NA'}</p>
      <div>
        {/* <Link to="/tickets">
          <Button variant="contained">Voltar</Button>
        </Link> 
        
        */}
        <Button variant="contained" onClick={() => navigate('/tickets')}>Voltar</Button>
        <Button variant="contained" onClick={() => navigate( `/tickets/${id}/edit`)}>Editar</Button>
      </div>
    </Stack>
  );
}

