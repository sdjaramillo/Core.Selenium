var comando = string.Empty;

string[] comandoSplit = comando.Split('=');
string target = comandoSplit[0];
string valor = string.Join("=", comandoSplit.Skip(1));

Console.ReadLine();