using FluentValidation;
using Grpc.Core;
using RateLimiter.Writer.DomainService.DTOs;
using RateLimiter.Writer.DomainService.Exceptions;
using RateLimiter.Writer.DomainService.Services.Abstractions;

namespace RateLimiter.Writer.GRPCServices;

public class WriterImpl : Writer.WriterBase
    {
        private readonly IWriterDomainService _writerDomainService;
        
        public WriterImpl(IWriterDomainService writerDomainService)
        {
            _writerDomainService = writerDomainService;
        }

        public override async Task<LimitDataReply> Create(CreateLimitRequest request, ServerCallContext context)
        {
            try
            {
                var requestDto = new RateLimitRequestDto
                {
                    Route = request.Route,
                    Rpm = request.Rpm
                };

                var responseDto = await _writerDomainService.CreateLimitAsync(requestDto);

                return new LimitDataReply 
                { 
                    Route = responseDto.Route, 
                    Rpm = responseDto.Rpm 
                };
            }
            catch (ValidationException ex)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, string.Join("; ", ex.Errors.Select(e => e.ErrorMessage))));
            }
            catch (RateLimitAlreadyExistsException ex)
            {
                throw new RpcException(new Status(StatusCode.AlreadyExists, ex.Message));
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, "Внутренняя ошибка сервера."));
            }
        }

        public override async Task<LimitDataReply> Get(GetLimitRequest request, ServerCallContext context)
        {
            try
            {
                var responseDto = await _writerDomainService.GetLimitAsync(request.Route);

                return new LimitDataReply 
                { 
                    Route = responseDto.Route, 
                    Rpm = responseDto.Rpm 
                };
            }
            catch (RateLimitNotFoundException ex)
            {
                throw new RpcException(new Status(StatusCode.NotFound, ex.Message));
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, "Внутренняя ошибка сервера."));
            }
        }

        public override async Task<SuccessReply> Update(UpdateLimitRequest request, ServerCallContext context)
        {
            try
            {
                var requestDto = new RateLimitRequestDto
                {
                    Route = request.Route,
                    Rpm = request.Rpm
                };
                
                bool result = await _writerDomainService.UpdateLimitAsync(requestDto);

                return new SuccessReply { Response = result };
            }
            catch (ValidationException ex)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, string.Join("; ", ex.Errors.Select(e => e.ErrorMessage))));
            }
            catch (RateLimitNotFoundException ex)
            {
                throw new RpcException(new Status(StatusCode.NotFound, ex.Message));
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, "Внутренняя ошибка сервера."));
            }
        }

        public override async Task<SuccessReply> Delete(DeleteLimitRequest request, ServerCallContext context)
        {
            try
            {
                bool result = await _writerDomainService.DeleteLimitAsync(request.Route);

                return new SuccessReply { Response = result };
            }
            catch (RateLimitNotFoundException ex)
            {
                throw new RpcException(new Status(StatusCode.NotFound, ex.Message));
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, "Внутренняя ошибка сервера."));
            }
        }
    }