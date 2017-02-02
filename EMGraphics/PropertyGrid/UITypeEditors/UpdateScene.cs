﻿namespace EMGraphics
{
    /// <summary>
    /// update and render the scene that contains specified <see cref="SceneObject"/>.
    /// </summary>
    public static class UpdateScene
    {
        /// <summary>
        /// update and render the scene that contains specified <paramref name="sceneObject"/>.
        /// </summary>
        /// <param name="sceneObject"></param>
        public static void UpdateAndRender(this SceneObject sceneObject)
        {
            while (sceneObject != null && sceneObject.Parent != null)
            {
                sceneObject = sceneObject.Parent.Content;
            }
            var rootObject = sceneObject as SceneRootObject;
            if (rootObject != null)
            {
                Scene scene = rootObject.BindingScene;
                ICanvas canvas = scene.Canvas;
                scene.UpdateOnce();
                canvas.Repaint();
            }
        }
    }
}