Feature: Gravar dados do usuario
	Como usuario
	Quero informar meus dados e ter o cadastro realizado

	Scenario: Cadastro
		Given dados passados
		When dados passados estao de acordo
		Then cadastro realizado

	Scenario: Cadastro com CPF ou outro dado invalido
		Given dados passados com CPF ou outro dado invalido
		When cadastro tentado
		Then uma excecao de validacao e lancada

