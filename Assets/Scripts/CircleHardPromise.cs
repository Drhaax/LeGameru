using System;


public interface ICircleHardPromise<T>
{
	ICircleHardPromise<T> OnSuccess(Action<T> action);


	ICircleHardPromise<T> OnFail(Action<string> failAction);
	
	void Resolve(T t);

	void ResolveWithFail(string errorMessage);
	
	bool ResolvedSuccessfully { get; }
	bool ResolvedWithFailure { get; }
	
}


public interface ICircleHardPromise
{
	ICircleHardPromise OnSuccess(Action action);


	ICircleHardPromise OnFail(Action<string> failAction);
	
	void Resolve();

	void ResolveWithFail(string errorMessage);
	
	bool ResolvedSuccessfully { get; }
	bool ResolvedWithFailure { get; }
}

public class CircleHardPromise<T> : ICircleHardPromise<T>
{
	private event Action<T> thenAction;
	private event Action<string> onFailAction;
	
	
	public ICircleHardPromise<T> OnSuccess(Action<T> action)
	{
		if (resolved)
		{
			if (result != null)
			{
				action(result);
			}

			return this;
		}

		thenAction -= action;
		thenAction += action;
		return this;
	}

	public ICircleHardPromise<T> OnFail(Action<string> failAction)
	{
		if (resolved)
		{
			if (failResult != null)
			{
				onFailAction(failResult);
			}

			return this;
		}

		onFailAction -= failAction;
		onFailAction += failAction;
		return this;
	}

	private bool resolved = false;

	private T result;

	private string failResult = null;
	
	public void Resolve(T t)
	{
		if (resolved)
		{
			throw new Exception("Cannot resolve promise more than once!");
		}

		resolved = true;
		result = t;
		
		thenAction?.Invoke(t);
	}
	
	public void ResolveWithFail(string errorMessage)
	{
		if (resolved)
		{
			throw new Exception("Cannot resolve promise more than once!");
		}
		
		resolved = true;
		failResult = errorMessage;
		
		onFailAction?.Invoke(errorMessage);
	}

	public bool ResolvedSuccessfully
	{
		get { return resolved && result != null; }
	}

	public bool ResolvedWithFailure 
	{
		get { return resolved && failResult != null; }
	}
}

public class CircleHardPromise : ICircleHardPromise
{
	private event Action thenAction;
	private event Action<string> onFailAction;
	
	
	public ICircleHardPromise OnSuccess(Action action)
	{
		if (resolved)
		{
			if (failResult == null)
			{
				action();
			}

			return this;
		}

		thenAction -= action;
		thenAction += action;
		return this;
	}

	public ICircleHardPromise OnFail(Action<string> failAction)
	{
		if (resolved)
		{
			if (failResult != null)
			{
				onFailAction(failResult);
			}

			return this;
		}

		onFailAction -= failAction;
		onFailAction += failAction;
		return this;
	}

	private bool resolved = false;


	private string failResult = null;
	
	public void Resolve()
	{
		if (resolved)
		{
			throw new Exception("Cannot resolve promise more than once!");
		}

		resolved = true;
		thenAction?.Invoke();
	}
	
	public void ResolveWithFail(string errorMessage)
	{
		if (resolved)
		{
			throw new Exception("Cannot resolve promise more than once!");
		}
		
		resolved = true;
		failResult = errorMessage;
		onFailAction?.Invoke(errorMessage);
	}
	
	public bool ResolvedSuccessfully
	{
		get { return resolved && failResult == null; }
	}

	public bool ResolvedWithFailure 
	{
		get { return resolved && failResult != null; }
	}
}
