Feature: As regras de criacao das entidades do dominio devem ser respeitadas
    Caso os valores informados estejam incorretos, excecoes devem ser lancadas.

	Scenario: Cadastro com Email informado invalido
		Given tentativa de criar objeto da entidade
		When alocado Email errado
		Then excecao gerada

	Scenario: Cadastro com CPF informado invalido
		Given tentativa de criar objeto da entidade
		When alocado CPF errado
		Then excecao gerada

	Scenario: Cadastro com Nome informado invalido
		Given tentativa de criar objeto da entidade
		When alocado Nome errado
		Then excecao gerada

	Scenario: Cadastro dados validos
		Given tentativa de criar objeto da entidade
		When alocado dados corretamente
		Then dado criado com sucesso

