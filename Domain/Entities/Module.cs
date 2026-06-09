using LunarColonySimulator.Domain.ValueObjects;

namespace LunarColonySimulator.Domain.Entities;

// Classe ABSTRATA — base de todo módulo construído na colônia.
// Não pode ser instanciada diretamente; obriga as filhas a implementarem
// os comportamentos específicos (produção/consumo).
public abstract class Module
{
    public string Id { get; }
    public string Name { get; }
    public Coordinates Location { get; }
    public DateTime BuiltAt { get; }
    public bool IsOperational { get; protected set; }

    // Atributo PRIVADO — só a própria classe consegue alterar.
    private int _maintenanceCycles;

    protected Module(string id, string name, Coordinates location)
    {
        Id = id;
        Name = name;
        Location = location;
        BuiltAt = DateTime.UtcNow;
        IsOperational = true;
        _maintenanceCycles = 0;
    }

    // Método ABSTRATO — cada módulo descreve sua função de forma única.
    public abstract string Describe();

    // Método VIRTUAL — comportamento padrão que filhas podem sobrescrever (polimorfismo).
    public virtual void PerformMaintenance()
    {
        _maintenanceCycles++;
        IsOperational = true;
    }

    public int MaintenanceCycles => _maintenanceCycles;
}
