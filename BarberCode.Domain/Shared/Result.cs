using System.Text.Json.Serialization;

namespace BarberCode.Domain.Shared;

public abstract class Result
{
	protected Result(ResultType type, string message)
	{
		Type = type;
		Message = message;
		IsSuccess = type == ResultType.Success;
	}

	public ResultType Type {  get; private set; }
	public string Message { get; private set; }
	public bool IsSuccess { get; private set; }
}

public enum ResultType
{
	Success,
	Validation,
	NotFound,
	Conflict,
	Failure

}

