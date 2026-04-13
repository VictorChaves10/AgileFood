using AgileFood.Business.Models.Users.Enums;

namespace AgileFood.Business.Models.Users;

public class User
{
    public long Id { get; private set; }

    public string Name { get; private set; }

    public string Email { get; private set; }

    public string PasswordHash { get; private set; }

    public UserRole Role { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime CreatedAt { get; private set; }

    protected User() { }

    public User(string name, string email, string passwordHash, UserRole role)
    {
        ChangeName(name);
        ChangeEmail(email);
        SetPasswordHash(passwordHash);
        Role = role;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string name, string email, UserRole role, bool isActive)
    {
        if (Name != name) ChangeName(name);
        if (Email != email) ChangeEmail(email);
        if (Role != role) ChangeRole(role);

        if (isActive)
            Activate();
        else
            Deactivate();
    }

    public void ChangeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("O nome do usuário é obrigatório.", nameof(name));

        Name = name;
    }

    public void ChangeEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("O e-mail é obrigatório.", nameof(email));

        Email = email;
    }

    public void SetPasswordHash(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("O hash da senha é obrigatório.", nameof(passwordHash));

        PasswordHash = passwordHash;
    }

    public void ChangeRole(UserRole role) => Role = role;

    public void Activate() => IsActive = true;

    public void Deactivate() => IsActive = false;
}