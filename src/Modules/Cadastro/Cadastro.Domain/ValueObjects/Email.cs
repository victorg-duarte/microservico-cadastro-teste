using Common.Entities;
using Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastro.Domain.ValueObjects;

public class Email : ValueObject
{
    public Email(string email)
    {
        Endereco = email;

        Validate();
    }

    public string Endereco { get; private set; }

    protected override void Validate()
    {
        string email = Endereco;
        if (email.IndexOf("@") <= 0)
        {

            throw new DomainNotificationException("O e-mail informado é inválido.");

        }
    }
}
