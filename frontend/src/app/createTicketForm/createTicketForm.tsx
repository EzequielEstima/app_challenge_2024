
import './createTicketForm.css'
import { Produto } from '../../dataModels/Produto';
import { Button, FormControl, FormHelperText, InputLabel, MenuItem, Select, Stack, TextField } from '@mui/material';
import { useState, useEffect } from 'react';
import { useForm } from 'react-hook-form';
import { TicketService } from '../../service/ticketService';
import { ProdutoService } from '../../service/produtoService';


type FormValues = {
  titulo: string,
  desc: string,
  prioridade: string,
  produto: string
}

export function CreateTicketForm1() {

  const [products, setProducts] = useState<Produto[]>([]);

  const form = useForm<FormValues>({
    defaultValues: {
      titulo: '',
      desc: '',
      prioridade: '',
      produto: ''
    }
  });

  const {register, handleSubmit, formState} = form;

  const {errors} = formState;

  useEffect(() => { 

    const productServer = new ProdutoService();

    productServer.getProdutos().then((produtos) => setProducts(produtos));

  }, []); 

  function handleCreateClick(data : FormValues ) {
    
    
    const ticketService = new TicketService();
    ticketService.createTicket({
      titulo: data.titulo,
      descricao: data.desc,
      prioridade: parseInt(data.prioridade),
      produtoId: parseInt(data.produto)
    })
  }

  return (
    <>
      <form onSubmit={handleSubmit(handleCreateClick)}>
        <Stack spacing={1.5} width={400}>
          <h2>Criar ticket</h2>

          <TextField id="titulo" label="Título" 
          {...register("titulo", {required : "Título é obrigatório"})}
          error={!!errors.titulo}
          helperText={errors.titulo?.message}
          />

          <TextField id="desc" label="Descrição" 
            {...register("desc", {required : "Descrição é obrigatória"})}
            error={!!errors.desc}
            helperText={errors.desc?.message}
          />
          
          <FormControl fullWidth>
            <InputLabel id="prioridade-select-label" error={!!errors.prioridade}>Prioridade</InputLabel>
            <Select 
            labelId="prioridade-select-label" 
            id="prioridade-select"
            {...register("prioridade",{required : 'Prioridade é obrigatória'})}
            error = {!!errors.prioridade}
            
            >
              <MenuItem value='1'>1</MenuItem>
              <MenuItem value='2'>2</MenuItem>
              <MenuItem value='3'>3</MenuItem>
              <MenuItem value='4'>4</MenuItem>
              <MenuItem value='5'>5</MenuItem>
            </Select>
            {!!errors.prioridade && <FormHelperText error>{errors.prioridade.message}</FormHelperText>}
            
          </FormControl>
        

          <FormControl fullWidth>
            <InputLabel id="produto-select-label" error={!!errors.produto}>Produto</InputLabel>
            <Select 
              labelId="produto-select-label" 
              id="produto-select" 
              {...register("produto", {required : 'Produto é obrigatório'})}
              error = {!!errors.produto}
            >
              {products.map((product) => (
                <MenuItem key={product.id} value={""+product.id}>{product.nome}</MenuItem>
              ))}
            </Select>
            {!!errors.produto && <FormHelperText error>{errors.produto.message}</FormHelperText>}
          </FormControl>

          <Button type="submit" variant="contained">Criar</Button>

        </Stack>  
      </form>
    </>
  );
}