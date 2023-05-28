using Domain.Exceptions.Shared;

namespace Domain.Exceptions.Channel {
    public class ChannelNotFoundException : AppException {
        public ChannelNotFoundException(string message) : base(message) {
        }
    }
}
