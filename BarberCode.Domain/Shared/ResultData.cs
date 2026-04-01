using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberCode.Domain.Shared
{
	//ResultData Para metodos que tem Retorno
	public class ResultData<T> : Result
	{
		public T? Data { get; private set; }
		public ResultData(T? data, ResultType type = ResultType.Success, string messege = "Criado Com Sucesso"
		)
		: base(type, messege)
		{
			Data = data;
		}
		public static ResultData<T> Success(T data)
			=> new(data);

		public static ResultData<T> Failure(ResultType type, string message)
			=> new(default, type, message);

	}

	//ResultData para void's( sem retorno)
	public class ResultData : Result
	{

		public ResultData(ResultType type = ResultType.Success, string messege = "Criado Com Sucesso")
		: base(type, messege)
		{
		}
		public static ResultData Success()
			=> new();

		public static ResultData Failure(ResultType type, string message)
			=> new(type, message);
	}

}
