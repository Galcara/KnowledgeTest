using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class ResponseMessage
    {
        public static Response CreateErrorResponse(Exception ex)
        {
            Response response = new Response();
            response.Success = false;
            response.ExceptionMessage = ex.Message;
            response.StackTrace = ex.StackTrace;
            response.Message = "Erro no banco de dados contate o administrador";
            response.Exception = ex;
            return response;
        }

        public static Response CreateSuccessResponse()
        {
            Response response = new Response();
            response.Success = true;
            response.Message = "Operação realizada com sucesso.";
            return response;
        }

        public static QueryResponse<T> QuerySuccessResponse<T>(List<T> item)
        {
            QueryResponse<T> response = new QueryResponse<T>();
            response.Success = true;
            response.Message = "Operação realizada com sucesso.";
            response.Data = item;
            return response;
        }

        public static QueryResponse<T> QueryErrorResponse<T>(Exception ex)
        {
            QueryResponse<T> response = new QueryResponse<T>();
            response.Success = false;
            response.Message = "Lista de Registros não encontrados";
            response.ExceptionMessage = ex.Message;
            response.StackTrace = ex.StackTrace;
            response.Exception = ex;
            return response;
        }

        public static SingleResponse<T> NotFoundData<T>()
        {
            SingleResponse<T> response = new SingleResponse<T>();
            response.Success = false;
            response.Message = "Registro não encontrado.";
            return response;
        }

        public static SingleResponse<T> ErrorFoundingData<T>(Exception ex)
        {
            SingleResponse<T> response = new SingleResponse<T>();
            response.Success = false;
            response.Message = "Registro não encontrado.";
            response.ExceptionMessage = ex.Message;
            response.StackTrace = ex.StackTrace;
            return response;
        }

        public static SingleResponse<T> SingleSuccessResponse<T>(T item)
        {
            SingleResponse<T> response = new SingleResponse<T>();
            response.Success = true;
            response.Message = "Operação realizada com sucesso.";
            response.Data = item;
            return response;
        }
       
    }
}
