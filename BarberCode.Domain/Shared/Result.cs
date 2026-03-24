namespace BarberCode.Domain.Shared;

public abstract class Result
{
	protected Result(ResultType type, string message)
	{
		Type = type;
		Message = message;
	}

	public ResultType Type {  get; private set; }
	public string Message { get; private set; }
}

public enum ResultType
{
	Success,
	Validation,
	NotFound,
	Conflict,
	Failure

}

public class ResultData<T> : Result 
{
	public T? Data { get; private set; }
	public ResultData(T? data, ResultType type = ResultType.Success,string messege = "Criado Com Sucesso") 
	: base (type,messege)
	{
		Data = data;
	}
	public static ResultData<T> Success(T data)
		=> new(data, ResultType.Success);

	public static ResultData<T> Failure(ResultType type, string message)
		=> new(default, type, message);

}
