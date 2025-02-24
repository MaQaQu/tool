﻿using SharpDX.Direct3D9;
using System;
using System.Windows;
using System.Windows.Interop;

namespace YouiToolkit.Design
{
    public class D3DImageSource : D3DImage, IDisposable
    {
        private static int ActiveClients;
        private static Direct3DEx D3DContext;
        private static DeviceEx D3DDevice;

        private Texture renderTarget;

        internal FpsLimiter FpsLimiter = new FpsLimiter(FpsLimiter._45Hz);

        public D3DImageSource()
        {
            StartD3D();
            ActiveClients++;
        }

        public void Dispose()
        {
            SetRenderTarget(null);

            Disposer.SafeDispose(ref renderTarget);

            ActiveClients--;
            EndD3D();
        }

        public void InvalidateD3DImage()
        {
            if (renderTarget != null)
            {
                if (FpsLimiter.Callabled())
                {
                    Lock();
                    AddDirtyRect(new Int32Rect(0, 0, PixelWidth, PixelHeight));
                    Unlock();
                }
            }
        }

        public void SetRenderTarget(SharpDX.Direct3D11.Texture2D target)
        {
            if (renderTarget != null)
            {
                renderTarget = null;

                Lock();
                SetBackBuffer(D3DResourceType.IDirect3DSurface9, IntPtr.Zero);
                Unlock();
            }

            if (target == null)
            {
                return;
            }

            var format = target.Description.Format switch
            {
                SharpDX.DXGI.Format.R10G10B10A2_UNorm => Format.A2B10G10R10,
                SharpDX.DXGI.Format.R16G16B16A16_Float => Format.A16B16G16R16F,
                SharpDX.DXGI.Format.B8G8R8A8_UNorm => Format.A8R8G8B8,
                _ => Format.Unknown,
            };
            using var resource = target.QueryInterface<SharpDX.DXGI.Resource>();
            var handle = resource.SharedHandle;

            if (!IsShareable(target))
            {
                throw new ArgumentException("Texture must be created with ResouceOptionFlags.Shared");
            }

            if (format == Format.Unknown)
            {
                throw new ArgumentException("Texture format is not compatible with OpenSharedResouce");
            }

            if (handle == IntPtr.Zero)
            {
                throw new ArgumentException("Invalid handle");
            }

            renderTarget = new Texture(D3DDevice, target.Description.Width, target.Description.Height, 1, Usage.RenderTarget, format, Pool.Default, ref handle);

            using (var surface = renderTarget.GetSurfaceLevel(0))
            {
                Lock();
                SetBackBuffer(D3DResourceType.IDirect3DSurface9, surface.NativePointer);
                Unlock();
            }
        }

        private void StartD3D()
        {
            if (ActiveClients != 0)
            {
                return;
            }

            var presentParams = GetPresentParameters();
            var createFlags = CreateFlags.HardwareVertexProcessing | CreateFlags.Multithreaded | CreateFlags.FpuPreserve | CreateFlags.EnablePresentStatistics;

            D3DContext = new Direct3DEx();
            D3DDevice = new DeviceEx(D3DContext, 0, DeviceType.Hardware, IntPtr.Zero, createFlags, presentParams);
        }

        private void EndD3D()
        {
            if (ActiveClients != 0)
            {
                return;
            }

            Disposer.SafeDispose(ref renderTarget);
            Disposer.SafeDispose(ref D3DDevice);
            Disposer.SafeDispose(ref D3DContext);
        }

        private static void ResetD3D()
        {
            if (ActiveClients == 0)
            {
                return;
            }
            var presentParams = GetPresentParameters();
            D3DDevice.ResetEx(ref presentParams);
        }

        private static PresentParameters GetPresentParameters()
        {
            var presentParams = new PresentParameters
            {
                Windowed = true,
                SwapEffect = SwapEffect.Discard,
                DeviceWindowHandle = NativeMethods.GetDesktopWindow(),
                PresentationInterval = PresentInterval.Default,
            };
            return presentParams;
        }

        private static bool IsShareable(SharpDX.Direct3D11.Texture2D texture)
        {
            return (texture.Description.OptionFlags & SharpDX.Direct3D11.ResourceOptionFlags.Shared) != 0;
        }
    }
}
