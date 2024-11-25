Feature: Obter dados de cadastro caso cadastro exista
	Como usuario
	Quero informar o CPF e obter os dados

	Scenario: Cadastro encontrado
		Given CPF foi passado corretamente
		When usuario e encontrado
		Then os dados do cadastro sao retornados

	Scenario: Cadastro nao encontrado
		Given CPF foi passado corretamente ou incorretamente
		When usuario nao e encontrado
		Then nada e retornado
