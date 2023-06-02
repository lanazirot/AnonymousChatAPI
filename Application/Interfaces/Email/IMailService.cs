namespace Application.Interfaces.Email;
public interface IMailService {
    void SendSupportMail(string subject, string body);
}

